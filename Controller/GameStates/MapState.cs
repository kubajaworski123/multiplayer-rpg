using MazeGame.Controller;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.GameStates;

public class MapState(IRenderer renderer, InputHandler? inputHandler) : GameState(renderer, inputHandler)
{
    public override void Enter()
    {
        Console.Clear();
        Render();
    }
    public override void Exit(){}

}
