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

        public void ApplyToBuilder(IMapBuilder builder)
        {
            builder.BuildFilledDungeon(40, 20);
            builder.BuildCentralHall(10, 5);
            builder.BuildChambers(5);
            builder.BuildCorridors(0);
            builder.AddItems(5);
            builder.AddWeapons(20);
           // builder.AddMobs(3);
            builder.AddArtifact();
            MobGroup humans = new MobGroup();
            MobGroup machines = new MobGroup();
            builder.AddMobGroup(() => new HumanMob(1, 1, humans, "Sith Acolyte", '!', 100, CharacterType.BountyHunter, 5, 2), 3);
            builder.AddMobGroup(() => new MachineMob(1, 1, machines, "Sith Droid", '!', 100, CharacterType.Droid, 6,1), 3);
        }
    }
}