using MazeGame.Controller.GameStates;
using MazeGame.View.Renderers;
using System;

namespace MazeGame.Controller;

public class MainController
{
    private readonly GameStateManager _stateManager;
    public static bool IsRunning { get; private set; } = true;

    public MainController(GameStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public static void StopRunning()
    {
        IsRunning = false;
    }

    public void Run()
    {
        while (IsRunning)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                _stateManager.HandleInput(keyInfo);
            }
        }
        _stateManager.PushState(new GameOverState(new GameOverRenderer()));
        Console.Clear();
    }
}