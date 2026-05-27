using System;
using System.Collections.Generic;

namespace MazeGame.View.Renderers
{
    internal class ThemeMessageRendrerer(string message) : IRenderer
    {
        private readonly string _msg = message;
        private List<string> _screenLines = new();

        public void Prepare()
        {
            int w = Console.WindowWidth - 1;
            int h = Console.WindowHeight;
            _screenLines.Clear();
            _screenLines.Add(ViewUtils.GenerateStarLine(w, ViewUtils.YellowCode, false, 0));
            for (int i = 0; i < h / 2 - 1; i++)
                _screenLines.Add(ViewUtils.GenerateStarLine(w, string.Empty, false, 1000 + i));
            _screenLines.Add(ViewUtils.GenerateStarLine(w, _msg, false, 5000));
            for (int i = 0; i < h / 2 - 2; i++)
                _screenLines.Add(ViewUtils.GenerateStarLine(w, string.Empty, false, 2000 + i));
            _screenLines.Add(ViewUtils.GenerateStarLine(w, ViewUtils.ResetCode, false, 9999));
        }

        public void Render()
        {
            ViewUtils.WriteFullScreen(_screenLines);
            Console.ReadKey(true);
        }
    }
}
