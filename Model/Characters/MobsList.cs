using System;
using System.Collections.Generic;

namespace MazeGame.Model.Characters
{
    public static class MobsList
    {
        private static List<Func<Mob>> Mobs = new List<Func<Mob>>()
        {

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