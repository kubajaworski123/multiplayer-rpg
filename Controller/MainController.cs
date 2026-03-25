using MazeGame.Controller.MainMapHandlers;
using MazeGame.Controller.UsersManualHandlers;
using MazeGame.Model;
using MazeGame.Model.Items;
using MazeGame.Model.Map;
using MazeGame.View;
using System;

namespace MazeGame.Controller;

public class MainController
{
    private readonly Player _player;
    private readonly Map _map;
    private readonly FrameRenderer _renderer;
    private readonly UsersManualRenderer _usersManualRenderer;

    private static InputHandler? _UsedInputChain;
    private static InputHandler? _MapInputChain;
    private static InputHandler? _UsersManualInputChain;

    private static bool _isRunning;

    public MainController(Player player, Map map, FrameRenderer renderer, InputHandler cor, UsersManualRenderer umRenderer)
    {
        _player = player;
        _map = map;
        _renderer = renderer;
        _isRunning = true;
        _MapInputChain = cor;
        _UsedInputChain = _MapInputChain;
        _usersManualRenderer = umRenderer;
        var exitUM = new ExitUsersManualHandler(_renderer);
        var scrollUp = new ScrollUpHandler(_usersManualRenderer);
        var scrollDown = new ScrollDownHandler(_usersManualRenderer);
        var exit = new ExitHandler();
        _UsersManualInputChain = exitUM;
        exitUM.SetNextHandler(exit); exit.SetNextHandler(scrollUp); scrollUp.SetNextHandler(scrollDown);
    }
    public static void StopRunning()
    {
        _isRunning = false;
    }
    public static void ChangeToMapHandler()
    {
        _UsedInputChain = _MapInputChain;
    }
    public static void ChangeToUmRenderer()
    {
        _UsedInputChain = _UsersManualInputChain;
    }
    public void Run()
    {
        _renderer.GetNewFrame();
        _renderer.RenderFrame();

        while (_isRunning)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                _UsedInputChain?.Handle(keyInfo);
            }
        }
        Console.Clear();
    }
}