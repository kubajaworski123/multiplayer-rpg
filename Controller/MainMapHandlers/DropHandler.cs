using MazeGame.Controller;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Items;
using MazeGame.Model.SoundPropagation;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Model.Players;
namespace MazeGame.Controller.MainMapHandlers;
public class DropHandler(Player player, Map map) : InputHandler
{
    private const int CONSOLE_KEY_D0 = 48;
    private const int CONSOLE_KEY_D9 = 57;
    private readonly Player _player = player;
    private readonly Map _map = map;
    private void Drop(int x)
    {
        if (_player.Backpack.Inventory.Count > x)
        {
            Item item = _player.Backpack.Inventory[x];
            item.Drop(_player, _map);
            _player.RefreshEquippedBonuses();
            if (item.NoiseRange > 0)
            {
                var soundData = new SoundData(_player.X, _player.Y, item.NoiseRange, _map, "drop");
                SoundPublisher.Instance.Publish(soundData);
            }
        }
    }
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        int keyCode = (int)keyInfo.Key;

        if (keyCode >= CONSOLE_KEY_D0 && keyCode <= CONSOLE_KEY_D9 && keyInfo.Modifiers == ConsoleModifiers.Alt)
        {
            int index = keyCode - CONSOLE_KEY_D0; 
            Drop(index);
        }
        else
        {
            base.Handle(keyInfo);
        }
    }
}