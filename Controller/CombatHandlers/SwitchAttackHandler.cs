using MazeGame.Model.Combat;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.CombatHandlers
{
    internal class SwitchAttackHandler(CombatRenderer renderer ,CombatSession session) : InputHandler
    {
        private CombatSession _session = session;
        private CombatRenderer _renderer = renderer;
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                _session.ScrollAtacksDown();
                _renderer.Prepare();
                _renderer.Render();
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                _session.ScrollAttacksUp();
                _renderer.Prepare();
                _renderer.Render();
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
