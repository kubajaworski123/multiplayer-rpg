using MazeGame.Controller;
using MazeGame.Controller.GameStates;
using System;

namespace MazeGame.Controller.UsersManualHandlers
{
    internal class ExitUsersManualHandler(GameStateManager stateManager) : InputHandler
    {
        private GameStateManager _stateManager = stateManager;

        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == KeyBinds.HelpKey)
            {
                _stateManager.PopState();
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}