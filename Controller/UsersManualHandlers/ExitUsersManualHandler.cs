using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MazeGame.Controller.UsersManualHandlers
{
    internal class ExitUsersManualHandler(FrameRenderer renderer): InputHandler
    {
        private FrameRenderer _renderer = renderer;
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.H)
            {
                MainController.ChangeToMapHandler();
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                _renderer.GetNewFrame();
                _renderer.RenderFrame();
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
