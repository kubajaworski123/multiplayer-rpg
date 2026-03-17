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
        //var map = new Map(40, 20);
        var CMB = new ConcreteMapBuilder();
        CMB.BuildFilledDungeon(40, 20);
        CMB.BuildCentralHall(10,5);
        CMB.BuildChambers(150000);
        CMB.BuildCorridors(0);
        CMB.BuildItems(5);
        CMB.BuildWeapons(20);

        var map = CMB.GetMap();
        Player player = new Player();
        var mainScreenRenderer = new MainScreenRenderer();
        mainScreenRenderer.CalculateMainScreen();
        mainScreenRenderer.RenderMainScreen();
        var renderer = new FrameRenderer();
        var controller = new MainController(player, map, renderer);
        controller.Run();
    }
}