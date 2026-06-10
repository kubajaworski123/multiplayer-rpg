using MazeGame.Controller;
using MazeGame.Model.Items;
using MazeGame.Model.Dungeon;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Swift;
using System.Text;
using MazeGame.Model.Players;
using MazeGame.Model.Observers;

namespace MazeGame.Controller.MainMapHandlers;

public class EquipHandler (Player player, Map map) : InputHandler
{
    private const int CONSOLE_KEY_D0 = 48;
    private const int CONSOLE_KEY_D9 = 57;
    private const int NO_MODIFIERS = 0;
    private readonly Player _player = player;
    private readonly Map _map = map;
    private void Equip(int x)
    {
        if (_player.Backpack.Inventory.Count > x)
        {
            Item item = _player.Backpack.Inventory[x];
            item.Equip(_player, _map);
            _player.RefreshEquippedBonuses();
            LogBus.Logs.Publish($"Equipped {item.Name}");
        }
    }

    public override void Handle(ConsoleKeyInfo keyInfo)
    { 
        int keyCode = (int)keyInfo.Key;

        if (keyCode >= CONSOLE_KEY_D0 && keyCode <= CONSOLE_KEY_D9 && keyInfo.Modifiers == NO_MODIFIERS)
        {
            int index = keyCode - CONSOLE_KEY_D0;
            Equip(index);
        }
        else
        {
            base.Handle(keyInfo);
        }
    }
}
