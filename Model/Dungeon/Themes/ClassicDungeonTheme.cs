using MazeGame.Model.Builders;
using MazeGame.Model.Characters;
using MazeGame.Model.Items;
using MazeGame.Model.Weapons;
using System;
using System.Collections.Generic;

namespace MazeGame.Model.Dungeon.Themes
{
    public class ClassicDungeonTheme : IDungeonThemeFactory
    {
        private readonly Random _random = new Random();

        public string WelcomeMessage => "YOU FEEL FEAR ENTERING THIS DARK AND DAMP DUNGEON...";

        private readonly List<Func<Item>> _dungeonItems = new List<Func<Item>>()
        {
            () => new Item("Rusty Key", 'k'),
            () => new Item("Silver Chalice", 'C'),
            () => new Item("Strange Gear", 'G'),
            () => new Item("Dusty Tome", 't')
        };

        private readonly List<Func<Weapon>> _dungeonWeapons = new List<Func<Weapon>>()
        {
            () => new HeavyWeapon("Gamorrean Axe", 'A', 22, true),
            () => new HeavyWeapon("Vibro-ax", 'X', 15, false),
            () => new LightWeapon("Hard Stick", 'g', 3, false),
            () => new LightWeapon("Stun Baton", 'b', 7, false)
        };

        private readonly List<Func<Mob>> _dungeonMobs = new List<Func<Mob>>()
        {
            () => new Mob("Bounty Hunter", '!', 100, 1, 1, CharacterBanners.BountyHunterBanner, 15, 10),
            () => new Mob("War Droid", '!', 100, 1, 1, CharacterBanners.DroidBanner, 20, 1)
        };

        public Item GetArtifact()
        {
            Item artifact = new Item("Golden Skull", '*');
            artifact.Rarity = ItemRarity.Artifact;
            return artifact;
        }

        public Item GetRandomItem()
        {
            int index = _random.Next(_dungeonItems.Count);
            Item freshItem = _dungeonItems[index]();
            return ItemFactory.ApplyRandomModifiers(freshItem);
        }

        public Item GetRandomWeapon()
        {
            int index = _random.Next(_dungeonWeapons.Count);
            Weapon freshWeapon = _dungeonWeapons[index]();
            return ItemFactory.ApplyRandomModifiers(freshWeapon);
        }

        public Mob GetRandomMob()
        {
            int index = _random.Next(_dungeonMobs.Count);
            return _dungeonMobs[index]();
        }

        public void ApplyToBuilder(IMapBuilder builder)
        {
            builder.BuildFilledDungeon(40, 20);
            builder.BuildChambers(150000);
            builder.BuildCorridors(0);
            builder.AddItems(5);
            builder.AddWeapons(20);
            builder.AddMobs(4);
            builder.AddArtifact();
        }
    }
}