using MazeGame.Controller;
using MazeGame.Model.Game;

namespace MazeGame.Networking;

public static class PlayerActionMapper
{
    public static PlayerAction? FromKey(int playerId, ConsoleKeyInfo key, bool inCombat)
    {
        if (inCombat)
        {
            return key.Key switch
            {
                ConsoleKey.Enter => new PlayerAction { PlayerId = playerId, Type = PlayerActionType.CombatConfirm },
                ConsoleKey.UpArrow => new PlayerAction { PlayerId = playerId, Type = PlayerActionType.CombatScrollUp },
                ConsoleKey.DownArrow => new PlayerAction { PlayerId = playerId, Type = PlayerActionType.CombatScrollDown },
                _ => new PlayerAction { PlayerId = playerId, Type = PlayerActionType.CombatConfirm }
            };
        }

        if (key.Key == KeyBinds.PickupKey)
            return new PlayerAction
            { 
                PlayerId = playerId,
                Type = PlayerActionType.Pickup 
            };
        if (key.Key == KeyBinds.ExitKey)
            return new PlayerAction 
            { 
                PlayerId = playerId,
                Type = PlayerActionType.Exit
            };

        int keyCode = (int)key.Key;
        if (keyCode >= 48 && keyCode <= 57 && key.Modifiers == 0)
            return new PlayerAction { PlayerId = playerId, Type = PlayerActionType.Equip, Idx = keyCode - 48 };
        if (keyCode >= 48 && keyCode <= 57 && key.Modifiers == ConsoleModifiers.Alt)
            return new PlayerAction 
            {
                PlayerId = playerId,
                Type = PlayerActionType.Drop,
                Idx = keyCode - 48 
            };

        var (dx, dy, move) = key.Key switch
        {
            var k when k == KeyBinds.ForwardKey => (0, -1, true),
            var k when k == KeyBinds.BackwardKey => (0, 1, true),
            var k when k == KeyBinds.LeftKey => (-1, 0, true),
            var k when k == KeyBinds.RightKey => (1, 0, true),
            _ => (0, 0, false)
        };
        if (move)
            return new PlayerAction 
            { 
              PlayerId = playerId,
              Type = PlayerActionType.Move,
              XMove = dx, YMove = dy 
            };
        return null;
    }
}
