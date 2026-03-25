using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.View
{
    internal class MapMenu
    {
        StringBuilder sb = new StringBuilder();
        private readonly List<string> _banner = new List<string>()
        {
            " ██████ ██   ██  ██████   ██████  ███████ ███████    █████    ███    ███  █████  ██████ ",
            "██      ██   ██ ██    ██ ██    ██ ██      ██        ██   ██   ████  ████ ██   ██ ██   ██",
            "██      ███████ ██    ██ ██    ██ ███████ █████     ███████   ██ ████ ██ ███████ ██████ ",
            "██      ██   ██ ██    ██ ██    ██      ██ ██        ██   ██   ██  ██  ██ ██   ██ ██     ",
            " ██████ ██   ██  ██████   ██████  ███████ ███████   ██   ██   ██      ██ ██   ██ ██     ",
            "CONFIRM WITH ENTER"
        };
        private readonly List<string> _maps = new List<string>()
        {
            "CLASSIC FULL DUNGEON",
            "NO CENTRAL HALL",
            "LESS ROOMS",
            "LESS ROOMS LONG CORRIDORS",
            "OPEN WORLD WITH A FEW ROOMS",
            "NO ITEMS DUNGEON"
        };

        public void CalculateMapMenuFarame(int highlitedOption)
        {
            sb.Clear();
            int bannerH = _banner.Count;
            int bannerL = _banner[0].Length;
            int drawWidth = Console.WindowWidth - 1;
            int linesToFillAtTheBottom = Console.WindowHeight - bannerH - _maps.Count - 3;
            sb.AppendLine(SubtitlesUtils.GenerateStarLine(drawWidth,"\x1b[93m"));
            foreach (string line in _banner)
            {
                sb.AppendLine(SubtitlesUtils.GenerateStarLine(drawWidth, line));
            }
            sb.AppendLine(SubtitlesUtils.GenerateStarLine(drawWidth, "\x1b[0m"));

            for (int i = 0; i < _maps.Count; i++)
            {
                sb.AppendLine(SubtitlesUtils.GenerateStarLine(drawWidth, _maps[i], i == highlitedOption? true : false));
            }
            for(int i = 0; i < linesToFillAtTheBottom; i++)
            {
                sb.AppendLine(SubtitlesUtils.GenerateStarLine(drawWidth, string.Empty));
            }
        }
        public int DisplayMenuFrame()
        {
            int highlighted = 0;
            bool confirmed = false;
            CalculateMapMenuFarame(highlighted);
            Console.Write(sb);
            while (!confirmed)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Enter:
                            confirmed = true;
                            break;
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.S:
                            highlighted = (highlighted + 1) % _maps.Count;
                            break;
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            highlighted = highlighted != 0 ? highlighted - 1 : _maps.Count-1;
                            break;
                    }
                    Console.SetCursorPosition(0, 0);
                    CalculateMapMenuFarame(highlighted);
                    Console.Write(sb);
                }
            }
            Console.Clear();
            return highlighted;
        }
    }
}
