using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.AllLogsHandlers
{
    public class ScrollAllLogsHandler(AllLogsRenderer renderer) : InputHandler
    {
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.W)
            {
                renderer.ScrollUp();
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.S)
            {
                renderer.ScrollDown();
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
