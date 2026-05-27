using MazeGame.View.Renderers;

namespace MazeGame.Controller.GameStates
{
    public class AllLogsState(IRenderer renderer, InputHandler? handler) : GameState(renderer, handler)
    {
        public override void Enter()
        {
            Render();
        }
    }
}