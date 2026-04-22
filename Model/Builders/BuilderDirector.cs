using MazeGame.Model.Dungeon.Themes;
using MazeGame.Model.Dungeon;
using System;

namespace MazeGame.Model.Builders
{
    public static class BuilderDirector
    {
        public static LevelData ProcessLevel(IDungeonThemeFactory theme)
        {
            var mapBuilder = new ConcreteMapBuilder(theme);
            var controlsBuilder = new ControlsListBuilder();
            var subtitlesBuilder = new StarWarsSubtitlesBuilder();
            var manualBuilder = new UsersManualBuilder();

            theme.ApplyGenerationStrategy(mapBuilder);
            theme.ApplyGenerationStrategy(controlsBuilder);
            theme.ApplyGenerationStrategy(subtitlesBuilder);
            theme.ApplyGenerationStrategy(manualBuilder);

            return new LevelData
            {
                Map = mapBuilder.GetMap(),
                Controls = controlsBuilder.GetControls(),
                Subtitles = subtitlesBuilder.GetSubtitles(),
                UsersManual = manualBuilder.GetUsersManual()
            };
        }
    }
}