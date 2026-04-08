using System;
using System.Collections.Generic;

namespace MazeGame.Controller.GameStates
{
    public class GameStateManager
    {
        private Stack<GameState> _states = new Stack<GameState>();

        public void PushState(GameState state)
        {
            if (_states.Count > 0)
            {
                _states.Peek().Exit();
            }

            _states.Push(state);
            _states.Peek().Enter();
        }

        public void PopState()
        {
            if (_states.Count > 0)
            {
                _states.Peek().Exit();
                _states.Pop();
            }

            if (_states.Count > 0)
            {
                _states.Peek().Enter();
            }
        }

        public void ChangeState(GameState state)
        {
            if (_states.Count > 0)
            {
                _states.Peek().Exit();
                _states.Pop();
            }
            _states.Push(state);
            _states.Peek().Enter();
        }

        public void HandleInput(ConsoleKeyInfo keyInfo)
        {
            if (_states.Count > 0)
            {
                _states.Peek().HandleInput(keyInfo);
            }
        }

        public void Render()
        {
            if (_states.Count > 0)
            {
                _states.Peek().Render();
            }
        }
    }
}