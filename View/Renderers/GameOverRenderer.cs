using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.View.Renderers
{
    internal class GameOverRenderer : IRenderer
    {
        private StringBuilder sb = new StringBuilder();
        public void Prepare()
        {
            sb.Clear();
            sb.AppendLine(ViewUtils.GenerateStarLine(Console.WindowWidth, "\x1b[93m", false, 0));
            foreach (var line in TextBanners.GAMEOVER)
            {
                sb.AppendLine(ViewUtils.GenerateStarLine(Console.WindowWidth, line, false, HashCode.Combine(line, 100)));
            }
            for (int i = TextBanners.GAMEOVER.Count + 2; i < Console.WindowHeight; i++) 
            {
                sb.AppendLine(ViewUtils.GenerateStarLine(Console.WindowWidth, string.Empty, false, 3000 + i));
            }
        }

        public void Render()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write(sb.ToString());
            Console.ReadKey(true);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }
    }
}
