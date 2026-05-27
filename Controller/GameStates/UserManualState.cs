using MazeGame.Controller;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.GameStates;

public class UserManualState(IRenderer renderer, InputHandler? handler) : GameState(renderer, handler)
{
    public override void Enter()
    {
        Render();
    }
}
