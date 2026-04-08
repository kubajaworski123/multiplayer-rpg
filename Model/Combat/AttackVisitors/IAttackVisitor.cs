using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Model.Items;
using MazeGame.Model.Weapons;

namespace MazeGame.Model.Combat
{
    public abstract class IAttackVisitor
    {
        public int CalculatedPlayerDefense { get; protected set; }
        public int CalculatedDamageToEnemy { get; protected set; }
        public abstract void Visit(HeavyWeapon core, Items.Item context);
        public abstract void Visit(LightWeapon core, Items.Item context);
        public abstract void Visit(MagicWeapon core, Items.Item context);
        public abstract void Visit(Item core, Item context);
    }
}