using MazeGame.Controller.GameStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.AllLogsHandlers
{
    public class ExitAllLogsHandler(GameStateManager stateManager) : InputHandler
    {
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.J)
            {
                stateManager.PopState();
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
