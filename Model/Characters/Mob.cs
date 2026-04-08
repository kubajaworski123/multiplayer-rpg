using MazeGame.Model.Combat;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MazeGame.Model.Characters
{
    public class Mob(string name = "unknown", char symbol = '!', int health = 100, int x = 1, int y = 1,
                     List<string>? banner = null, int attack = 1, int armour = 1)
                     : Character(name,symbol,health,x,y,banner), ICombat
    {
        public int Damage { get; protected set; } = attack;
        public int DamageVariation => Math.Max(1, Damage/3);
        public int Armour { get; protected set; } = armour;
        public override string ColorCode => "\x1b[31m";
        public void Attack(ICombat target)
        {
            target.ReciveDamage(Damage);
        }
        public void ReciveDamage(int damage)
        {
            int realDamage = damage - Armour;
            if (realDamage > 0)
            {
                ModifyHealth(-realDamage);
            }
        }
    }
}