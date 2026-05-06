using MazeGame.Model.Dungeon;

namespace MazeGame.Model.SoundPropagation
{
    public class SoundData
    {
        public int SourceX { get; }
        public int SourceY { get; }
        public int Range { get; }
        public Map MapContext { get; }
        public string SourceName { get; }

        public SoundData(int x, int y, int range, Map map, string sourceName)
        {
            SourceX = x; SourceY = y; Range = range; MapContext = map; SourceName = sourceName;
        }
    }
}