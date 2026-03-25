using System;
namespace MazeGame.Controller.MainMapHandlers;
public class ExitHandler() : InputHandler
{
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            MainController.StopRunning();
        }
        else
        {
            base.Handle(keyInfo);
        }
    }
}