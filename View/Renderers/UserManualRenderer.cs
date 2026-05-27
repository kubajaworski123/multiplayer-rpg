using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.View.Renderers
{
    public class UserManualRenderer(List<string> usersManualList) : IRenderer
    {
        private StringBuilder sb = new StringBuilder();
        private List<string> usersManual = usersManualList;
        private List<string> _formattedLines = new List<string>();
        private int _scrollOffset = 0;

        public void Prepare()
        {
            int width = Console.WindowWidth - 1;
            _formattedLines.Clear();

            _formattedLines.Add(ViewUtils.GenerateStarLine(width, "\x1b[93m"));
            foreach (string line in usersManual)
            {
                _formattedLines.Add(ViewUtils.GenerateStarLine(width, line));
            }
            _formattedLines.Add(ViewUtils.GenerateStarLine(width, "\x1b[0m"));
        }

        public void Render()
        {
            var screenLines = new List<string>(Console.WindowHeight);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                int lineIndex = _scrollOffset + i;
                screenLines.Add(lineIndex < _formattedLines.Count
                    ? _formattedLines[lineIndex]
                    : ViewUtils.GenerateStarLine(Console.WindowWidth, string.Empty, false, i + 1));
            }
            ViewUtils.WriteFullScreen(screenLines);
        }

        public void ScrollUp()
        {
            if (_scrollOffset > 0)
            {
                _scrollOffset--;
            }
        }

        public void ScrollDown()
        {
            int maxOffset = Math.Max(0, _formattedLines.Count - Console.WindowHeight);
            if (_scrollOffset < maxOffset)
            {
                _scrollOffset++;
            }
        }

        public void ResetScroll()
        {
            _scrollOffset = 0;
        }
    }
}