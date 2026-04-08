using MazeGame.Controller;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.UsersManualHandlers
{
    internal class ScrollDownHandler(UserManualRenderer umR) : InputHandler
    {
        private UserManualRenderer usersManualRenderer = umR;
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.S)
            {
                umR.ScrollDown();
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
