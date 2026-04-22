using MazeGame.Model.Dungeon;
using MazeGame.View;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using MazeGame.Model.Players;
using MazeGame.Model.Observers;

namespace MazeGame.Controller;

public class UnknownInputHandler(Map map, Player player, LogBus logBus) : InputHandler
{
    Map _map = map;
    Player _player = player;
    LogBus _logbus = logBus;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        _logbus.Publish($"Detected unknown key: {keyInfo.Key}");
    }
}