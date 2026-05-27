using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.GameStates
{
    public class CombatState(IRenderer renderer, InputHandler? handler = null) : GameState(renderer, handler)
    {
        public override void Enter()
        {
            Render();
        }
    }
}
