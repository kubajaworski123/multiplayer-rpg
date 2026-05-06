using MazeGame.Model.Builders;
using MazeGame.Model.Characters;
using MazeGame.Model.Items;
using MazeGame.Model.Weapons;
using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Dungeon.Themes
{
    public class SurfaceTheme : IDungeonThemeFactory
    {
        private Random _random = new Random();
        private readonly List<Func<Item>> _surfaceItems = new List<Func<Item>>()
        {
            () => new Item("Surface Photo", 's'),
            () => new Item("Animal skin", 'a'),
            () => new Item("Dry Plant", 'p'),
        };
        private readonly List<Func<Weapon>> _surfaceWeapons = new List<Func<Weapon>>()
        {
            () => new MagicWeapon("Force Medalion", 'M', 10, false),
            () => new MagicWeapon("Force Staff", 'A', 15, true),
            () => new LightWeapon("Steel Dagger", 'D', 6, false),
            () => new LightWeapon("Wooden Staff", 'W', 12, true),
            () => new HeavyWeapon("Tree Branch", 'B', 8, true),
            () => new HeavyWeapon("Tribal Mace", 'S', 14, false),
        };
        public string WelcomeMessage => "YOU ARE ON THE SURFACE OF MINI!";

        public Item GetArtifact()
        {
            Item DungeonKey = new Item("Dungeon Key",'*');
            DungeonKey.Rarity = ItemRarity.Artifact;
            return DungeonKey;
        }

        public Item GetRandomItem()
        {
            int index = _random.Next(_surfaceItems.Count);
            Item freshItem = _surfaceItems[index]();
            return ItemFactory.ApplyRandomModifiers(freshItem);
        }

        public Item GetRandomWeapon()
        {
            int index = _random.Next(_surfaceWeapons.Count);
            Weapon freshWeapon = _surfaceWeapons[index]();
            return ItemFactory.ApplyRandomModifiers(freshWeapon);
        }



        public void ApplyToBuilder(IMapBuilder builder)
        {
            builder.BuildEmptyDungeon(40, 20);
            builder.BuildChambers(4);
            builder.BuildCorridors(0);
            builder.AddItems(5);
            builder.AddWeapons(10);
            builder.AddMobs(4);
            builder.AddArtifact();
            MobGroup humans = new MobGroup();
            MobGroup machines = new MobGroup();
            builder.AddMobGroup(() => new HumanMob(1, 1, humans, "Surface Explorer", '!', 100, CharacterType.BountyHunter, 5, 2), 2);
            builder.AddMobGroup(() => new MachineMob(1, 1, machines, "Imperial War Machine", '!', 100, CharacterType.Machine, 4, 3), 2);
            builder.AddMobGroup(() => new MachineMob(1, 1, machines, "Droid Soldier", '!', 100, CharacterType.Droid, 5, 2), 2);
        }
    }
}