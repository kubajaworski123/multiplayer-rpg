using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Model.Items;
namespace MazeGame.Controller;
public class DropHandler(Player player, Action<int> action) : InputHandler
{
    private readonly Player _player = player;
    private readonly Action<int> _onDrop = action;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        switch ((keyInfo.Key, keyInfo.Modifiers ))
        {
            case (ConsoleKey.D1,ConsoleModifiers.Alt) :
                _onDrop?.Invoke(1);
                break;
            case (ConsoleKey.D2, ConsoleModifiers.Alt):
                _onDrop?.Invoke(2);
                break;
            case (ConsoleKey.D3,ConsoleModifiers.Alt):
                _onDrop?.Invoke(3);
                break;
            case (ConsoleKey.D4,ConsoleModifiers.Alt):
                _onDrop?.Invoke(4);
                break;
            case (ConsoleKey.D5,ConsoleModifiers.Alt):
                _onDrop?.Invoke(5);
                break;
            case (ConsoleKey.D6,ConsoleModifiers.Alt):
                _onDrop?.Invoke(6);
                break;
            case (ConsoleKey.D7,ConsoleModifiers.Alt):
                _onDrop?.Invoke(7);
                break;
            case (ConsoleKey.D8,ConsoleModifiers.Alt):
                _onDrop?.Invoke(8);
                break;
            case (ConsoleKey.D9,ConsoleModifiers.Alt):
                _onDrop?.Invoke(9);
                break;
            case (ConsoleKey.D0,ConsoleModifiers.Alt):
                _onDrop?.Invoke(0);
                 break;
            default:
                base.Handle(keyInfo);
                break;
        }
    }
}