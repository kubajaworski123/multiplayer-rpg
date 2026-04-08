using MazeGame.Controller;
using MazeGame.Controller.GameStates;
using System;

namespace MazeGame.Controller.MainMapHandlers
{
    internal class UsersManualHandler(GameStateManager stateManager, UserManualState manualState) : InputHandler
    {
        public static ConsoleKey ManualKey = ConsoleKey.H;
        private GameStateManager _stateManager = stateManager;
        private UserManualState _manualState = manualState;

        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == KeyBinds.HelpKey)
            {
                _stateManager.PushState(_manualState);
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}