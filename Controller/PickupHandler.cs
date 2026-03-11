using MazeGame.Model.Items;
using MazeGame.Model.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller;

public class PickupHandler(Player player, Map map, Action onPickup) : InputHandler
{
    private readonly Player _player = player;
    private readonly Map _map = map;
    private readonly Action _onPickup = onPickup;

    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.E)
        {
            if (_map.Tiles[_player.X,_player.Y].MapEntities.Count != 0)
            {
                _onPickup?.Invoke();
            }
        }
        else
        {
            base.Handle(keyInfo);
        }
    }
}