using MazeGame.Model.Builders;
using MazeGame.Model.Characters;
using MazeGame.Model.Items;
using MazeGame.Model.Weapons;
using System;
using System.Collections.Generic;

namespace MazeGame.Model.Dungeon.Themes
{
    public class SithRuinsTheme : IDungeonThemeFactory
    {
        private readonly Random _random = new Random();

        public string WelcomeMessage => "THE DARK SIDE OF THE FORCE ENVELOPS THESE ANCIENT RUINS...";

        private readonly List<Func<Item>> _sithItems = new List<Func<Item>>()
        {
            () => new Item("Sith Holocron", 'h'),
            () => new Item("Broken Lightsaber Hilt", 'b'),
            () => new Item("Dark Cloak", 'c')
        };

        private readonly List<Func<Weapon>> _sithWeapons = new List<Func<Weapon>>()
        {
            () => new MagicWeapon("Sith Lightsaber", 'l', 35, false),
            () => new MagicWeapon("Double-bladed Lightsaber", 'L', 40, true),
            () => new LightWeapon("Vibro-dagger", 'd', 10, false)
        };

        private readonly List<Func<Mob>> _sithMobs = new List<Func<Mob>>()
        {
            () => new Mob("Sith Droid", '!', 100, 1, 1, CharacterBanners.DroidBanner, 20, 1),
            () => new Mob("Sith Soldier", '!', 100, 1, 1, CharacterBanners.BountyHunterBanner, 15, 10)
        };

        public Item GetArtifact()
        {
            Item artifact = new Item("Sith Crystal", '*');
            artifact.Rarity = ItemRarity.Artifact;
            return artifact;
        }

        public Item GetRandomItem()
        {
            int index = _random.Next(_sithItems.Count);
            Item freshItem = _sithItems[index]();
            return ItemFactory.ApplyRandomModifiers(freshItem);
        }

        public Item GetRandomWeapon()
        {
            int index = _random.Next(_sithWeapons.Count);
            Weapon freshWeapon = _sithWeapons[index]();
            return ItemFactory.ApplyRandomModifiers(freshWeapon);
        }

        public Mob GetRandomMob()
        {
            int index = _random.Next(_sithMobs.Count);
            return _sithMobs[index]();
        }

        public void ApplyGenerationStrategy(IMapBuilder builder)
        {
            builder.BuildFilledDungeon(40, 20);
            builder.BuildCentralHall(10, 5);
            builder.BuildChambers(5);
            builder.BuildCorridors(0);
            builder.AddItems(5);
            builder.AddWeapons(20);
            builder.AddMobs(3);
            builder.AddArtifact();
        }
    }
}