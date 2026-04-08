using MazeGame.Controller;
using MazeGame.Model.Dungeon;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Model.MapEntities;
using MazeGame.Model.Players;

namespace MazeGame.Controller.MainMapHandlers;

public class PickupHandler(Player player, Map map, FrameRenderer renderer) : InputHandler
{
    private readonly Player _player = player;
    private readonly Map _map = map;
    private readonly FrameRenderer _renderer = renderer;
    public static ConsoleKey PickupKey = ConsoleKey.E;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == KeyBinds.PickupKey)
        {
            if (_map[_player.X,_player.Y].MapEntities.Count != 0)
            {
                MapEntity item = _map[_player.X, _player.Y].MapEntities.First();
                item.PickUp(_player);
                _map[_player.X, _player.Y].RemoveMapEntity(item);
                _renderer.Prepare();
                _renderer.Render();
            }
        }
        else
        {
            base.Handle(keyInfo);
        }
    }
}