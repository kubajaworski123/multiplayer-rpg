using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.View.Renderers
{
    public class AllLogsRenderer(List<string> logs) : IRenderer
    {
        private StringBuilder _sb = new StringBuilder();
        private List<string> _logs = logs;
        private List<string> _formattedLines = new List<string>();
        private int _scrollOffset = 0;

        public void Prepare()
        {
            int width = Console.WindowWidth - 1;
            _formattedLines.Clear();

            _formattedLines.Add(ViewUtils.GenerateStarLine(width, "\x1b[93m"));
            _formattedLines.Add(ViewUtils.GenerateStarLine(width, "--- LOGGER ---"));
            _formattedLines.Add(ViewUtils.GenerateStarLine(width, "PRESS J TO EXIT"));
            _formattedLines.Add(ViewUtils.GenerateStarLine(width, " "));

            if (_logs.Count == 0)
            {
                _formattedLines.Add(ViewUtils.GenerateStarLine(width, "No logs available."));
            }
            else
            {
                foreach (string line in _logs)
                {
                    _formattedLines.Add(ViewUtils.GenerateStarLine(width, line));
                }
            }

            _formattedLines.Add(ViewUtils.GenerateStarLine(width, "\x1b[0m"));
        }

        public void Render()
        {
            _sb.Clear();
            int displayHeight = Console.WindowHeight;

            for (int i = 0; i < displayHeight - 1; i++)
            {
                int lineIndex = _scrollOffset + i;

                if (lineIndex < _formattedLines.Count)
                {
                    _sb.AppendLine(_formattedLines[lineIndex]);
                }
                else
                {
                    _sb.AppendLine(ViewUtils.GenerateStarLine(Console.WindowWidth - 1, string.Empty));
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(_sb);
        }

        public void ScrollUp()
        {
            if (_scrollOffset > 0) _scrollOffset--;
        }

        public void ScrollDown()
        {
            int maxOffset = Math.Max(0, _formattedLines.Count - Console.WindowHeight + 1);
            if (_scrollOffset < maxOffset) _scrollOffset++;
        }

        public void ResetScroll()
        {
            _scrollOffset = 0;
        }
    }
}