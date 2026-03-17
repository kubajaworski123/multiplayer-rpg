using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace MazeGame.Controller;

public class UnknownInputHandler(Action<ConsoleKeyInfo> action) : InputHandler
{
    private readonly Action<ConsoleKeyInfo> _onUnknownInputDetection = action;
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        _onUnknownInputDetection?.Invoke(keyInfo);
    }
}