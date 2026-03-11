using MazeGame.Controller;
using MazeGame.Model;
using MazeGame.Model.Items;
using MazeGame.Model.Map;
using MazeGame.View;
using System;

namespace MazeGame;
  
class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var map = new Map(40, 20);
        Player player = new Player();
        var renderer = new FrameRenderer();
        var controller = new MainController(player, map, renderer);
        controller.Run();
    }
}