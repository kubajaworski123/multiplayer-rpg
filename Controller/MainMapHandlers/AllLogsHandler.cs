using MazeGame.Controller.GameStates;
using System;

namespace MazeGame.Controller.MainMapHandlers
{
    public class AllLogsHandler(GameStateManager stateManager, AllLogsState allLogsState) : InputHandler
    {
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == KeyBinds.LogsKey)
            {
                stateManager.PushState(allLogsState);
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}