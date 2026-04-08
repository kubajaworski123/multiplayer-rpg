using MazeGame.Controller;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.GameStates;
public abstract class GameState(IRenderer renderer, InputHandler? inputHandler)
{
    protected IRenderer _renderer = renderer;
    protected InputHandler? _inputHandler = inputHandler;
    public abstract void Enter();
    public abstract void Exit();
    public virtual void Render()
    {
        _renderer?.Prepare();
        _renderer?.Render();
    }
    public virtual void HandleInput(ConsoleKeyInfo keyInfo)
    {
        _inputHandler?.Handle(keyInfo);
    }
}
