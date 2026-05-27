using MazeGame.Model.Combat;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Game;
using MazeGame.Model.Items;
using MazeGame.Model.Players;
using MazeGame.Networking.Dto;

namespace MazeGame.Networking;

public static class GameStateMapper
{
    public static GameStateDto ToDto(GameWorld world)
    {
        lock (world.Sync)
        {
            return new GameStateDto
            {
                GameStateN = world.GameStateN,
                IsRunning = world.IsRunning,
                Controls = new List<string>(world.Controls),
                RecentLogs = new List<string>(world.RecentLogs),
                AllLogs = new List<string>(world.AllLogs),
                UsersManual = new List<string>(world.UsersManual),
                Map = MapToDto(world.Map, world.Players),
                Players = world.Players.Select(PlayerToDto).ToList(),
                Combats = world.Combats.Select(kv => CombatToDto(kv.Key, kv.Value)).ToList()
            };
        }
    }

    private static MapDto MapToDto(Map map, List<NetworkPlayer> players)
    {
        var dto = new MapDto { Width = map.Width, Height = map.Height };
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                var tile = map[x, y];
                dto.Tiles.Add(new TileDto
                {
                    X = x,
                    Y = y,
                    IsWall = tile.IsWall,
                    Symbol = tile.Symbol,
                    Entities = tile.MapEntities
                        .SelectMany(e => e.GetItemsOnTile())
                        .Select(ItemMapper.ToDto)
                        .ToList()
                });
            }
        }

        foreach (var player in players)
        {
            if (!player.IsConnected) continue;
            dto.Combatants.Add(CombatantFrom(player));
        }

        foreach (var combatant in map.Combatants)
        {
            if (combatant.Living)
                dto.Combatants.Add(CombatantFrom(combatant));
        }

        return dto;
    }

    private static CombatantDto CombatantFrom(ICombat combatant)
    {
        var dto = new CombatantDto
        {
            Name = combatant.Name,
            Symbol = combatant.Symbol,
            Type = combatant.Type.ToString(),
            X = combatant.X,
            Y = combatant.Y,
            Health = combatant.Health,
            IsPlayer = combatant.NetworkPlayerId.HasValue
        };
        if (combatant.NetworkPlayerId.HasValue)
            dto.PlayerId = combatant.NetworkPlayerId.Value;
        return dto;
    }

    private static PlayerDto PlayerToDto(NetworkPlayer p) => new()
    {
        Id = p.PlayerId,
        Symbol = p.Symbol,
        Name = p.Name,
        X = p.X,
        Y = p.Y,
        Health = p.Health,
        IsConnected = p.IsConnected,
        Stats = new StatsDto
        {
            Health = p.Health,
            Strength = p.Stats.Strength,
            Agility = p.Stats.Agility,
            Wisdom = p.Stats.Wisdom,
            Happiness = p.Stats.Happiness,
            Agressiveness = p.Stats.Agressiveness,
            Luck = p.Stats.Luck
        },
        Backpack = new BackpackDto
        {
            InventoryCapacity = p.Backpack.InventoryCapacity,
            Gold = p.Backpack.Gold,
            Money = p.Backpack.Money,
            Inventory = p.Backpack.Inventory.Select(ItemMapper.ToDto).ToList()
        },
        LeftHand = p.LeftHand != null ? ItemMapper.ToDto(p.LeftHand) : null,
        RightHand = p.RightHand != null ? ItemMapper.ToDto(p.RightHand) : null
    };

    private static CombatDto CombatToDto(int playerId, CombatSession s) => new()
    {
        PlayerId = playerId,
        LeftName = s.LeftActor.Name,
        RightName = s.RightActor.Name,
        LeftHealth = s.LeftActor.Health,
        RightHealth = s.RightActor.Health,
        LeftSymbol = s.LeftActor.Symbol,
        RightSymbol = s.RightActor.Symbol,
        LeftType = s.LeftActor.Type.ToString(),
        RightType = s.RightActor.Type.ToString(),
        ActiveCombat = s.ActiveCombat,
        Turn = s.Turn,
        HighlightedAttack = s.HighlightedAttackOtption,
        AttackNames = s.PossibleAttacks.Select(a => a.Name).ToList(),
        Outcome = s.Outcome.ToString()
    };
}

public static class ItemMapper
{
    public static ItemDto ToDto(Item item)
    {
        var modifiers = new List<string>();
        item.CollectModifierTags(modifiers);
        return new ItemDto
        {
            Name = item.Name,
            Symbol = item.Symbol,
            Rarity = item.Rarity.ToString(),
            Damage = item.Damage,
            IsTwoHanded = item.IsTwoHanded,
            NoiseRange = item.NoiseRange,
            Kind = item.Kind,
            Modifiers = modifiers
        };
    }
}
