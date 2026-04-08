using MazeGame.Model.Players;
using MazeGame.Model.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Combat
{
    public class StealthAttackVisitor: IAttackVisitor
    {

        private Player _player;
        public StealthAttackVisitor(Player player)
        {
            _player = player;
        }

        public override void Visit(HeavyWeapon core, Items.Item context)
        {
            CalculatedDamageToEnemy = context.Damage/2;
            CalculatedPlayerDefense = _player.Stats.Strength;
        }

        public override void Visit(LightWeapon core, Items.Item context)
        {
            CalculatedDamageToEnemy = 2*context.Damage;
            CalculatedPlayerDefense = _player.Stats.Agility;
        }

        public override void Visit(MagicWeapon core, Items.Item context)
        {
            CalculatedDamageToEnemy = 1;
            CalculatedPlayerDefense = 0;
        }

        public override void Visit(Items.Item core, Items.Item context)
        {
            CalculatedDamageToEnemy = 0;
            CalculatedPlayerDefense = 0;
        }
    }
}

