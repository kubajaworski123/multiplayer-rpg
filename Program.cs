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

        var mainScreenRenderer = new WelcomeScreen();
        var subRenderer = new SubtitlesRenderer();

        mainScreenRenderer.CalculateWelcomeScreen();
        mainScreenRenderer.RenderMainScreen();


        var mapMenu = new MapMenu();
        int chosenMap = mapMenu.DisplayMenuFrame();

        (Map map, List<string> controls, List<string> intro, List<string> usersManual) = BuilderDirector.ProcessMapChoice(chosenMap);
        Player player = new Player();
        var renderer = new FrameRenderer(player, map, controls);
        var umRenderer = new UsersManualRenderer(usersManual);
        umRenderer.CalculateUsersManual();
        var COR = new CORMapBuilder(player, map, renderer, umRenderer);
        BuilderDirector.MenuOptionsMap[chosenMap](COR);
        var cor = COR.GetCOR();

        var ct = new CancellationTokenSource();
        Task.Run(() => SubtitlesUtils.PlayImperialMarchLooped(ct.Token));
        subRenderer.DisplaySubs(intro);
        ct.Cancel();

        var controller = new MainController(player, map, renderer,cor, umRenderer);
        controller.Run();
        
    }
}