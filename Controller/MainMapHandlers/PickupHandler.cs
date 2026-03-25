using MazeGame.Model.Items;
using MazeGame.Model.Map;
using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.MainMapHandlers;

public class PickupHandler(Player player, Map map, FrameRenderer renderer) : InputHandler
{
    private readonly Player _player = player;
    private readonly Map _map = map;
    private readonly FrameRenderer _renderer = renderer;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.E)
        {
            if (_map.Tiles[_player.X,_player.Y].MapEntities.Count != 0)
            {
                MapEntity item = _map.Tiles[_player.X, _player.Y].MapEntities.First();
                item.PickUp(_player);
                _map.Tiles[_player.X, _player.Y].RemoveMapEntity(item);
                _renderer.GetNewFrame();
                _renderer.RenderFrame();
            }
        }
        else
        {
            base.Handle(keyInfo);
        }
    }
}