using MazeGame.Model.Characters;
using MazeGame.Model.Combat;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Items;
using MazeGame.Model.MapEntities;
using MazeGame.Model.Observers;
using MazeGame.Model.Players;
using MazeGame.Model.SoundPropagation;

namespace MazeGame.Model.Game;

public class GameWorld
{
    private readonly object _sync = new();
    internal object Sync => _sync;
    public Map Map { get; }
    public List<string> Controls { get; }
    public List<string> UsersManual { get; }
    public List<string> AllLogs { get; } = new();
    public List<NetworkPlayer> Players { get; } = new();
    public Dictionary<int, CombatSession> Combats { get; } = new();
    public List<string> RecentLogs { get; } = new();
    public int GameStateN { get; private set; }
    public bool IsRunning { get; private set; } = true;
    private readonly Random _spawnRandom = new();

    public GameWorld(Map map, List<string> controls, List<string> usersManual)
    {
        Map = map;
        Controls = controls;
        UsersManual = usersManual;
    }

    public int AddPlayer(string name)
    {
        lock (_sync)
        {
            if (Players.Count >= 9) return -1;
            int id = -1;
            for (int i = 1; i <= 9; i++)
            {
                if (!Players.Any(p => p.PlayerId == i))
                {
                    id = i;
                    break;
                }
            }
            if (id < 0) return -1;
            var player = new NetworkPlayer(id);
            player.SetName(name);
            var spawn = FindSpawnPosition();
            player.X = spawn.x;
            player.Y = spawn.y;
            Map.AddCombatant(player);
            Players.Add(player);
            SetStateAsNew();
            return id;
        }
    }

    public void RemovePlayer(int playerId)
    {
        lock (_sync)
        {
            var player = GetPlayer(playerId);
            if (player == null) return;
            Map.RemoveCombatant(player);
            Players.Remove(player);
            Combats.Remove(playerId);
            SetStateAsNew();
        }
    }

    public GameActionResult ProcessAction(PlayerAction action)
    {
        lock (_sync)
        {
            if (!IsRunning) return GameActionResult.Rejected;
            var player = GetPlayer(action.PlayerId);
            if (player == null || !player.IsConnected) return GameActionResult.Rejected;

            if (Combats.ContainsKey(action.PlayerId))
            {
                return ProcessCombatAction(player, action);
            }

            return action.Type switch
            {
                PlayerActionType.Move => TryMove(player, action.XMove, action.YMove),
                PlayerActionType.Pickup => TryPickup(player),
                PlayerActionType.Equip => TryEquip(player, action.Idx),
                PlayerActionType.Drop => TryDrop(player, action.Idx),
                PlayerActionType.Exit => ExitGame(),
                _ => GameActionResult.Rejected
            };
        }
    }

    public NetworkPlayer? GetPlayer(int id) => Players.FirstOrDefault(p => p.PlayerId == id);

    public CombatSession? GetCombat(int playerId) =>
        Combats.TryGetValue(playerId, out var s) ? s : null;

    private GameActionResult TryMove(NetworkPlayer player, int dx, int dy)
    {
        if (dx == 0 && dy == 0) return GameActionResult.Rejected;
        int newX = player.X + dx;
        int newY = player.Y + dy;
        if (!CanMoveTo(newX, newY, player.PlayerId))
            return GameActionResult.Rejected;

        player.X = newX;
        player.Y = newY;
        var combatants = Map.GetCombatantsFromTile(newX, newY).Where(c => c.Living).ToList();

        if (combatants.Count > 0)
        {
            var enemy = combatants[0];
            if (Combats.Values.Any(s => s.RightActor == enemy))
                return GameActionResult.Rejected;

            var attacks = new List<IAttackVisitor>
            {
                new NormalAttackVisitor(player),
                new StealthAttackVisitor(player),
                new MagicAttackVisitor(player)
            };
            Combats[player.PlayerId] = new CombatSession(player, enemy, attacks, LogBus.Logs);
            PublishLog($"{player.Name} engages {enemy.Name}");
            SetStateAsNew();
            return GameActionResult.CombatStarted;
        }

        MoveMobsForAllPlayers();
        SetStateAsNew();
        return GameActionResult.Ok;
    }

    private GameActionResult TryPickup(NetworkPlayer player)
    {
        var tile = Map[player.X, player.Y];
        if (tile.MapEntities.Count == 0) return GameActionResult.Rejected;
        MapEntity entity = tile.MapEntities.First();
        bool picked = entity.PickUp(player);
        if (!picked)
        {
            PublishLog($"{player.Name} cant pick {entity.Name}");
            return GameActionResult.Rejected;
        }
        PublishLog($"Picked {entity.Name}");
        if (entity.NoiseRange > 0)
        {
            var soundData = new SoundData(player.X, player.Y, entity.NoiseRange, Map, "pickup");
            SoundPublisher.Instance.Publish(soundData);
        }
        tile.RemoveMapEntity(entity);
        SetStateAsNew();
        return GameActionResult.Ok;
    }

    private GameActionResult TryEquip(NetworkPlayer player, int index)
    {
        if (index < 0 || index >= player.Backpack.Inventory.Count) 
            return GameActionResult.Rejected;
        Item item = player.Backpack.Inventory[index];
        item.Equip(player, Map);
        PublishLog($"Equipped {item.Name}");
        SetStateAsNew();
        return GameActionResult.Ok;
    }

    private GameActionResult TryDrop(NetworkPlayer player, int index)
    {
        if (index < 0 || index >= player.Backpack.Inventory.Count) return GameActionResult.Rejected;
        Item item = player.Backpack.Inventory[index];
        item.Drop(player, Map);
        SetStateAsNew();
        return GameActionResult.Ok;
    }

    private GameActionResult ProcessCombatAction(NetworkPlayer player, PlayerAction action)
    {
        if (!Combats.TryGetValue(action.PlayerId, out var session))
            return GameActionResult.Rejected;

        if (!session.ActiveCombat)
        {
            if (session.LeftActor.Health <= 0)
            {
                player.IsConnected = false;
                Map.RemoveCombatant(player);
                Combats.Remove(player.PlayerId);
                if (Players.All(p => !p.IsConnected || p.Health <= 0))
                    IsRunning = false;
                SetStateAsNew();
                return GameActionResult.PlayerDead;
            }
            if (session.RightActor.Health <= 0)
            {
                session.RightActor.SetType(CharacterType.Dead);
                Map.RemoveCombatant(session.RightActor);
            }
            Combats.Remove(player.PlayerId);
            SetStateAsNew();
            return GameActionResult.Ok;
        }

        switch (action.Type)
        {
            case PlayerActionType.CombatScrollUp:
                session.ScrollAttacksUp();
                SetStateAsNew();
                return GameActionResult.Ok;
            case PlayerActionType.CombatScrollDown:
                session.ScrollAtacksDown();
                SetStateAsNew();
                return GameActionResult.Ok;
            case PlayerActionType.CombatConfirm:
                var visitor = session.PossibleAttacks[session.HighlightedAttackOtption];
                player.CurrentAttackStyle = visitor;
                session.PlayersTurn();
                if (!session.ActiveCombat)
                {
                    if (session.RightActor.Health <= 0)
                    {
                        session.RightActor.SetType(CharacterType.Dead);
                        Map.RemoveCombatant(session.RightActor);
                    }
                    else if (session.LeftActor.Health <= 0)
                    {
                        player.IsConnected = false;
                        Map.RemoveCombatant(player);
                        Combats.Remove(player.PlayerId);
                        SetStateAsNew();
                        return GameActionResult.PlayerDead;
                    }
                    Combats.Remove(player.PlayerId);
                }
                SetStateAsNew();
                return GameActionResult.Ok;
            default:
                return GameActionResult.Rejected;
        }
    }

    private GameActionResult ExitGame()
    {
        IsRunning = false;
        SetStateAsNew();
        return GameActionResult.GameExit;
    }

    private bool CanMoveTo(int x, int y, int movingPlayerId)
    {
        if (x < 0 || x >= Map.Width || y < 0 || y >= Map.Height) return false;
        if (Map.Tiles[x, y].IsWall) return false;
        foreach (var p in Players)
        {
            if (p.PlayerId != movingPlayerId && p.X == x && p.Y == y && p.Health > 0)
                return false;
        }
        return true;
    }

    private void MoveMobsForAllPlayers()
    {
        var occupied = Players.Where(p => p.Health > 0).Select(p => (p.X, p.Y)).ToHashSet();
        foreach (var combatant in Map.Combatants.ToList())
        {
            if (combatant.Living)
                combatant.FindPosAndMove(Map, occupied);
        }
    }

    private (int x, int y) FindSpawnPosition()
    {
        var positions = Map.GetNonWallPositions()
            .Where(p => !Players.Any(pl => pl.X == p.x && pl.Y == p.y))
            .Where(p => !Map.GetCombatantsFromTile(p.x, p.y).Any(c => c.BlocksSpawnTile))
            .ToList();
        if (positions.Count == 0) return (1, 1);
        return positions[_spawnRandom.Next(positions.Count)];
    }

    private void PublishLog(string message) => LogBus.Logs.Publish(message);

    public void AppendLog(string message)
    {
        lock (_sync)
        {
            RecentLogs.Add(message);
            if (RecentLogs.Count > 50) RecentLogs.RemoveAt(0);
            AllLogs.Add(message);
            SetStateAsNew();
        }
    }

    private void SetStateAsNew() => GameStateN++;
}
