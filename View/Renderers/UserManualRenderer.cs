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
            sb.Clear();
            int displayHeight = Console.WindowHeight;

            for (int i = 0; i < displayHeight -1; i++)
            {
                int lineIndex = _scrollOffset + i;

                if (lineIndex < _formattedLines.Count)
                {
                    sb.AppendLine(_formattedLines[lineIndex]);
                }
                else
                {
                    sb.AppendLine(ViewUtils.GenerateStarLine(Console.WindowWidth - 1, string.Empty));
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(sb);
        }

        public void ScrollUp()
        {
            if (_scrollOffset > 0)
            {
                _scrollOffset--;
                Render();
            }
        }

        public void ScrollDown()
        {
            int maxOffset = Math.Max(0, _formattedLines.Count - Console.WindowHeight);
            if (_scrollOffset < maxOffset)
            {
                _scrollOffset++;
                Render();
            }
        }

        public void ResetScroll()
        {
            _scrollOffset = 0;
        }
    }
}