using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Characters.PlayerComponents
{
    public class Attributes(int str = 1, int agi = 1, int wis = 1, int hp = 100, int hap = 3, int agr = 1, int luc = 1)
    {
        public int Strength { get; private set; } = str;
        public int Agility { get; private set; } = agi;
        public int Wisdom { get; private set; } = wis;
        public int Health { get; private set; } = hp;
        public int Happiness { get; private set; } = hap;
        public int Agressiveness { get; private set; } = agr;
        public int Luck { get; private set; } = luc;

        public void ModifyStrength(int amount)
        {
            Strength += amount;
            if (Strength < 0) Strength = 0;
        }
        public void ModifyAgility(int amount)
        {
            Agility += amount;
            if (Agility < 0) Agility = 0;
        }
        public void ModifyWisdom(int amount)
        {
            Wisdom += amount;
            if (Wisdom < 0) Wisdom = 0;
        }
        public void ModifyHappiness(int amount)
        {
            Happiness += amount;
            if (Happiness < 0) Happiness = 0;
        }
        public void ModifyAgressiveness(int amount)
        {
            Agressiveness += amount;
            if (Agressiveness < 0) Agressiveness = 0;
        }
        public void ModifyLuck(int amount)
        {
            Luck += amount;
            if (Luck < 0) Luck = 0;
        }

        public void ModifyHealth(int amount)
        {
            Health += amount;
            if (Health < 0) Health = 0;
        }
    }
}