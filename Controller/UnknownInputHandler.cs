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

public class UnknownInputHandler(Map map, Player player) : InputHandler
{
    Map _map = map;
    Player _player = player;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        LogBus.Logs.Publish($"Detected unknown key: {keyInfo.Key}");
    }
}