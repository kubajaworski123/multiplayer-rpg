using MazeGame.Controller;
using MazeGame.Controller.GameStates;
using System;
namespace MazeGame.Controller.MainMapHandlers;
public class ExitHandler(GameStateManager stateManager, GameOverState gameOverState) : InputHandler
{
    public static ConsoleKey exitKey = ConsoleKey.Escape;
    private GameStateManager _stateManager = stateManager;
    private GameOverState _gameOverState = gameOverState;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == KeyBinds.ExitKey)
        {
            _stateManager.PushState(_gameOverState);
        }
        else
        {
            base.Handle(keyInfo);
        }
    }
}