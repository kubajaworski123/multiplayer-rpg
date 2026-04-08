using MazeGame.Controller;
using MazeGame.Controller.GameStates;
using MazeGame.Controller.MainMapHandlers;
using MazeGame.Controller.UsersManualHandlers;
using MazeGame.Model.Builders;
using MazeGame.Model.Characters;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Players;
using MazeGame.View;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MazeGame;

class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Player player = new Player();

        player.SetBanner(CharacterBanners.PlayerBanner);
        var mainScreenRenderer = new WelcomeScreenRenderer();
        mainScreenRenderer.Prepare();
        mainScreenRenderer.Render();

        var mapMenu = new MapMenu();
        int chosenMap = mapMenu.DisplayMenuFrame();

        (Map map, List<string> controls, List<string> intro, List<string> usersManual) = BuilderDirector.ProcessMapChoice(chosenMap);

        var subRenderer = new SWIntroRenderer(intro);
        var ct = new CancellationTokenSource();
        Task.Run(() => ViewUtils.PlayImperialMarchLooped(ct.Token));
        subRenderer.Prepare();
        subRenderer.Render();
        ct.Cancel();

        var renderer = new FrameRenderer(player, map, controls);
        var umRenderer = new UserManualRenderer(usersManual);
        var gameOverRenderer = new GameOverRenderer();

        umRenderer.Prepare();
        var stateManager = new GameStateManager();

        var exitUM = new ExitUsersManualHandler(stateManager);
        var manualState = new UserManualState(umRenderer, exitUM);
        var gameOverState = new GameOverState(gameOverRenderer, null);

        var scrollUp = new ScrollUpHandler(umRenderer);
        var scrollDown = new ScrollDownHandler(umRenderer);

        var exitFromManual = new ExitHandler(stateManager, gameOverState);

        exitUM.SetNextHandler(exitFromManual);
        exitFromManual.SetNextHandler(scrollUp);
        scrollUp.SetNextHandler(scrollDown);
        var corBuilder = new CORMapBuilder(player, map, renderer, stateManager, manualState, gameOverState);
        BuilderDirector.MenuOptionsMap[chosenMap](corBuilder);
        var mapInputChain = corBuilder.GetCOR();

        var mapState = new MapState(renderer, mapInputChain);

        stateManager.PushState(mapState);

        var controller = new MainController(stateManager);
        controller.Run();
    }
}