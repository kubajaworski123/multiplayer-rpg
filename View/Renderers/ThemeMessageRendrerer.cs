using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.View.Renderers
{
    internal class ThemeMessageRendrerer(string message) : IRenderer
    {
        private string _msg = message;
        StringBuilder sb = new StringBuilder();
        public void Prepare()
        {
            sb.Clear();
            int h = Console.WindowHeight;
            for(int i = 0; i < h/2 - 1; i++)
            {
                sb.AppendLine(ViewUtils.GenerateStarLine(Console.WindowWidth - 1, string.Empty));
            }

            sb.AppendLine(ViewUtils.GenerateStarLine(Console.WindowWidth,_msg));

            for (int i = 0; i < h / 2 - 1; i++)
            {
                sb.AppendLine(ViewUtils.GenerateStarLine(Console.WindowWidth - 1, string.Empty));
            }
        }

        public void Render()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write(sb);
            Console.ReadKey();
            Console.Clear();
        }
    }
}
