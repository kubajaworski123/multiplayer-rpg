using MazeGame.Model.Dungeon;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Builders
{
    public class LevelData
    {
        public Map Map { get; set; }
        public List<string> Controls { get; set; }
        public List<string> Subtitles { get; set; }
        public List<string> UsersManual { get; set; }
    }
}
