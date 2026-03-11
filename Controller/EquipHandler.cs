using MazeGame.Model.Items;
using MazeGame.Model.Map;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Swift;
using System.Text;

namespace MazeGame.Controller;

public class EquipHandler (Player player, Action<int> action) : InputHandler
{
    private readonly Player _player = player;
    private readonly Action<int> _onPickup = action;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Modifiers == 0)
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    _onPickup?.Invoke(1);
                    break;
                case ConsoleKey.D2:
                    _onPickup?.Invoke(2);
                    break;
                case ConsoleKey.D3:
                    _onPickup?.Invoke(3);
                    break;
                case ConsoleKey.D4:
                    _onPickup?.Invoke(4);
                    break;
                case ConsoleKey.D5:
                    _onPickup?.Invoke(5);
                    break;
                case ConsoleKey.D6:
                    _onPickup?.Invoke(6);
                    break;
                case ConsoleKey.D7:
                    _onPickup?.Invoke(7);
                    break;
                case ConsoleKey.D8:
                    _onPickup?.Invoke(8);
                    break;
                case ConsoleKey.D9:
                    _onPickup?.Invoke(9);
                    break;
                case ConsoleKey.D0:
                    _onPickup?.Invoke(0);
                    break;
                default:
                    base.Handle(keyInfo);
                    break;
            }
        else
        {
            base.Handle(keyInfo);
        }
    }
}
