using MazeGame.Controller;
using MazeGame.Controller.CombatHandlers;
using MazeGame.Controller.GameStates;
using MazeGame.Model;
using MazeGame.Model.Combat;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Observers;
using MazeGame.Model.Players;
using MazeGame.View.Renderers;
using System;

namespace MazeGame.Controller.MainMapHandlers;

public class MovementHandler(Player player, Map map, GameStateManager manager, LogBus logbus) : InputHandler
{
    private readonly Player _player = player;
    private readonly Map _map = map;
    private readonly GameStateManager _stateManager = manager;
    private readonly LogBus _logbus = logbus;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        var (dx, dy, isMovementKey) = keyInfo.Key switch
        {
            var k when k == KeyBinds.ForwardKey => (0, -1, true),
            var k when k == KeyBinds.BackwardKey => (0, 1, true),
            var k when k == KeyBinds.LeftKey => (-1, 0, true),
            var k when k == KeyBinds.RightKey => (1, 0, true),
            _ => (0, 0, false)
        };

        if (isMovementKey)
        {
            int newX = _player.X + dx;
            int newY = _player.Y + dy;

            if (CanMoveTo(newX, newY))
            {
                _player.X = newX;
                _player.Y = newY;
                var combatants = _map.GetCombatantsFromTile(newX, newY);
                if (combatants.Count > 0)
                {
                    var enemy = combatants[0];
                    var attacks = new List<IAttackVisitor>()
                    {
                        new NormalAttackVisitor(_player),
                        new StealthAttackVisitor(_player),
                        new MagicAttackVisitor(_player)
                    };
                    var combatSession = new CombatSession(_player, enemy, attacks, _logbus);
                    var combatRenderer = new CombatRenderer(_player, enemy, combatSession);

                    var switchHandler = new SwitchAttackHandler(combatSession);
                    var choseHandler = new ChooseAttackHandler(_player, combatSession,_stateManager,_map);
                    choseHandler.SetNextHandler(switchHandler);

                    var combatState = new CombatState(combatRenderer, choseHandler);

                    _stateManager.PushState(combatState);
                }
            }
        }
        else
        {
            base.Handle(keyInfo);
        }
    }

    private bool CanMoveTo(int x, int y)
    {
        if (x < 0 || x >= _map.Width || y < 0 || y >= _map.Height)
        {
            return false;
        }

        if (_map.Tiles[x, y].IsWall)
        {
            _logbus.Publish($"{_player.Name} cant move to ({x}, {y})");
            return false;
        }
        return true;
    }
}