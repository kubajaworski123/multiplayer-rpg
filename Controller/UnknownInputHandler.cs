using MazeGame.Model.Dungeon;
using MazeGame.View;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using MazeGame.Model.Players;

namespace MazeGame.Controller;

public class UnknownInputHandler(Map map, Player player, FrameRenderer renderer) : InputHandler
{
    Map _map = map;
    Player _player = player;
    FrameRenderer _renderer = renderer;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        Logger.SingletonLogger.Add($"Unknown key: {keyInfo.Key}");
        _renderer.Prepare();
        _renderer.Render();
    }
}