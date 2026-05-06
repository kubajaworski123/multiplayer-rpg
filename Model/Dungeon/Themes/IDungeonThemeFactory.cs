using MazeGame.Model.Builders;
using MazeGame.Model.Characters;
using MazeGame.Model.Items;
using MazeGame.Model.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Dungeon.Themes
{
    public interface IDungeonThemeFactory
    {
        string WelcomeMessage { get; }
        Item GetArtifact();
        Item GetRandomItem();
        Item GetRandomWeapon();
        void ApplyToBuilder(IMapBuilder builder);
    }
}
