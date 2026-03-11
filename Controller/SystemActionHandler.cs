using System;
namespace MazeGame.Controller;
public class SystemActionHandler(Action onExit) : InputHandler
{
    private readonly Action _onExit = onExit;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            _onExit?.Invoke();
        }
        else
        {
            base.Handle(keyInfo);
        }
    }
}