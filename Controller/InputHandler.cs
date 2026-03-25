using System;
using System.Collections.Generic;
using System.Text;
namespace MazeGame.Controller;
public abstract class InputHandler
{
    protected InputHandler? nextHandler;
    public InputHandler SetNextHandler(InputHandler handler)
    {
        nextHandler = handler;
        return handler;
    }
    public virtual void Handle(ConsoleKeyInfo keyInfo)
    {
        if (nextHandler != null)
        {
            nextHandler.Handle(keyInfo);
        }
    }
}