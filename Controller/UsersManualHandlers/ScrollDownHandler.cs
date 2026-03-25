using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.UsersManualHandlers
{
    internal class ScrollDownHandler(UsersManualRenderer umR) : InputHandler
    {
        private UsersManualRenderer usersManualRenderer = umR;
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
