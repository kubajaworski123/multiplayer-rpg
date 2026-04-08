using MazeGame.Controller;
using MazeGame.Model.Items;
using MazeGame.Model.Dungeon;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Model.Players;
namespace MazeGame.Controller.MainMapHandlers;
public class DropHandler(Player player, Map map, FrameRenderer renderer) : InputHandler
{
    private readonly Player _player = player;
    private readonly FrameRenderer _renderer = renderer;
    private readonly Map _map = map;
    public void Drop(int x)
    {
        if (_player.Backpack.Inventory.Count > x)
        {
            Item item = _player.Backpack.Inventory[x];
            item.Drop(_player, _map);
            _renderer.Prepare();
            _renderer.Render();
        }
    }
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        switch ((keyInfo.Key, keyInfo.Modifiers ))
        {
            case (ConsoleKey.D1,ConsoleModifiers.Alt) :
                Drop(1);
                break;
            case (ConsoleKey.D2, ConsoleModifiers.Alt):
                Drop(2);
                break;
            case (ConsoleKey.D3,ConsoleModifiers.Alt):
                Drop(3);
                break;
            case (ConsoleKey.D4,ConsoleModifiers.Alt):
                Drop(4);
                break;
            case (ConsoleKey.D5,ConsoleModifiers.Alt):
                Drop(5);
                break;
            case (ConsoleKey.D6,ConsoleModifiers.Alt):
                Drop(6);
                break;
            case (ConsoleKey.D7,ConsoleModifiers.Alt):
                Drop(7);
                break;
            case (ConsoleKey.D8,ConsoleModifiers.Alt):
                Drop(8);
                break;
            case (ConsoleKey.D9,ConsoleModifiers.Alt):
                Drop(9);
                break;
            case (ConsoleKey.D0,ConsoleModifiers.Alt):
                Drop(0);
                 break;
            default:
                base.Handle(keyInfo);
                break;
        }
    }
}