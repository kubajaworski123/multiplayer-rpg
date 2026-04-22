using MazeGame.Model.Combat;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.CombatHandlers
{
    internal class SwitchAttackHandler(CombatSession session) : InputHandler
    {
        private CombatSession _session = session;
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                _session.ScrollAtacksDown();
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                _session.ScrollAttacksUp();
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
