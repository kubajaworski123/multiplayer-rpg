using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.MainMapHandlers
{
    internal class UsersManualHandler(UsersManualRenderer umRenderer) : InputHandler
    {
        private UsersManualRenderer _usersManualRenderer = umRenderer;
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.H)
            {
                _usersManualRenderer.DisplayUsersManual();
                MainController.ChangeToUmRenderer();
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
