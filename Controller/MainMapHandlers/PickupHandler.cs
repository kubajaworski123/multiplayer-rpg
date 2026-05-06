using MazeGame.Controller;
using MazeGame.Model.Dungeon;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Model.MapEntities;
using MazeGame.Model.Players;
using MazeGame.View;
using MazeGame.Model.Observers;
using MazeGame.Model.SoundPropagation;

namespace MazeGame.Controller.MainMapHandlers;

public class PickupHandler(Player player, Map map) : InputHandler
{
    private readonly Player _player = player;
    private readonly Map _map = map;
    public static ConsoleKey PickupKey = ConsoleKey.E;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == KeyBinds.PickupKey)
        {
            if (_map[_player.X,_player.Y].MapEntities.Count != 0)
            {
                MapEntity item = _map[_player.X, _player.Y].MapEntities.First();
                bool picked = item.PickUp(_player);
                if (picked)
                {
                    LogBus.Logs.Publish($"Picked {item.Name}");
                    if(item.NoiseRange > 0)
                    {
                        SoundData soundData = new SoundData(_player.X, _player.Y, item.NoiseRange, _map, $"pickup");
                        SoundPublisher.Instance.Publish(soundData);
                    }
                    _map[_player.X, _player.Y].RemoveMapEntity(item);
                }
                else
                    LogBus.Logs.Publish($"{_player.Name} cant pick {item.Name}");
            }
        }
        else
        {
            base.Handle(keyInfo);
        }
    }
}