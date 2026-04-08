using System;
using System.Collections.Generic;

namespace MazeGame.Model.Characters
{
    public static class MobsList
    {
        private static List<Func<Mob>> Mobs = new List<Func<Mob>>()
        {
            () => new Mob("Imperial War Machine", '!', 100, 1, 1, CharacterBanners.ATSTBanner, 10, 5),
            () => new Mob("Bounty Hunter", '!', 100, 1, 1, CharacterBanners.BountyHunterBanner, 15, 10),
            () => new Mob("War Droid", '!', 100, 1, 1, CharacterBanners.DroidBanner, 20, 1)
        };

        public static Mob GetRandomMob()
        {
            Random rnd = new Random();
            int index = rnd.Next(Mobs.Count);
            return Mobs[index]();
        }
    }
}