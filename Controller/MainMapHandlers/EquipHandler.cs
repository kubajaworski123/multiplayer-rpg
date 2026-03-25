using MazeGame.Model.Items;
using MazeGame.Model.Map;
using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Swift;
using System.Text;

namespace MazeGame.Controller.MainMapHandlers;

public class EquipHandler (Player player, Map map, FrameRenderer renderer) : InputHandler
{
    private readonly Player _player = player;
    private readonly FrameRenderer _renderer = renderer;
    private readonly Map _map = map;
    private void Equip(int x)
    {
        if (_player.Inventory.Count > x)
        {
            Item item = _player.Inventory[x];
            item.Equip(_player, _map);
            _renderer.GetNewFrame();
            _renderer.RenderFrame();
        }
    }

    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Modifiers == 0)
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    Equip(1);
                    break;
                case ConsoleKey.D2:
                    Equip(2);
                    break;
                case ConsoleKey.D3:
                    Equip(3);
                    break;
                case ConsoleKey.D4:
                    Equip(4);
                    break;
                case ConsoleKey.D5:
                    Equip(5);
                    break;
                case ConsoleKey.D6:
                    Equip(6);
                    break;
                case ConsoleKey.D7:
                    Equip(7);
                    break;
                case ConsoleKey.D8:
                    Equip(8);
                    break;
                case ConsoleKey.D9:
                    Equip(9);
                    break;
                case ConsoleKey.D0:
                    Equip(0);
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
