using MazeGame.Model.Dungeon.Themes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Configuration
{
    public static class ThemeProcessor
    {
        private static readonly Dictionary<string, Func<IDungeonThemeFactory>> Themes = new Dictionary<string, Func<IDungeonThemeFactory>>()
        {
            { "Surface", () => new SurfaceTheme() },
            { "SithRuins", () => new SithRuinsTheme() },
            { "ClassicDungeon", () => new ClassicDungeonTheme() }
        };

        public static IDungeonThemeFactory GetTheme(string name)
        {
            if (Themes.TryGetValue(name, out var factory))
            {
                return factory();
            }
            return new ClassicDungeonTheme();
        }
    }
}
