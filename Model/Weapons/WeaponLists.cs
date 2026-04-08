using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Weapons
{
    internal static class WeaponLists
    {

        public static List<LightWeapon> LightWeapons = new List<LightWeapon>()
        {
            new LightWeapon("Vibroblade", 'v', 8, false),
            new LightWeapon("Electrostaff", 'e', 10, true),
            new LightWeapon("Wrist Blade", 'w', 6, false),
            new LightWeapon("Hard Stick", 'g', 3, false),
            new LightWeapon("Stun Baton", 'b', 7, false)
        };

        public static List<HeavyWeapon> HeavyWeapons = new List<HeavyWeapon>()
        {
            new HeavyWeapon("Gamorrean Axe", 'A', 22, true),
            new HeavyWeapon("Wookiee Warblade", 'W', 25, true),
            new HeavyWeapon("Vibro-ax", 'X', 10, false),
            new HeavyWeapon("Electropole", 'P', 18, true),
            new HeavyWeapon("Lightsaber Pike", 'S', 24, true)
        };

        public static List<MagicWeapon> MagicWeapons = new List<MagicWeapon>()
        {
            new MagicWeapon("Lightsaber", 'l', 30, false),
            new MagicWeapon("Double Lightsaber", 'L', 35, true),
            new MagicWeapon("Darksaber", 'd', 32, false),
            new MagicWeapon("Sith Sword", 's', 28, false),
            new MagicWeapon("Magic Stick", 'r', 5, false)
        };
        public static Weapon GetRandomWeapon()
        {
            var all = new List<Weapon>();
            all.AddRange(LightWeapons);
            all.AddRange(HeavyWeapons);
            all.AddRange(MagicWeapons);

            System.Random rnd = new System.Random();
            return all[rnd.Next(all.Count)];
        }
    }
}
