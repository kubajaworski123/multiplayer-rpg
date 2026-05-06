using MazeGame.Model.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Weapons
{
    public class MagicWeapon(string name, char symbol, int damage, bool isTwoHanded) : Weapon(name, symbol, damage, isTwoHanded)
    {
        public override void Accept(Combat.IAttackVisitor visitor, Item context)
        {
            visitor.Visit(this, context);
        }
        public override int NoiseRange => 8;
    }
}
