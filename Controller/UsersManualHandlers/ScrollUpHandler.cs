using MazeGame.Controller;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MazeGame.Controller.UsersManualHandlers
{
    internal class ScrollUpHandler(UserManualRenderer umR) : InputHandler
    {
        private UserManualRenderer usersManualRenderer = umR;
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.W)
            {
                umR.ScrollUp();
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
