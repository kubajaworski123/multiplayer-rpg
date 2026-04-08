using MazeGame.Model.Players;
using MazeGame.Model.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Combat
{
    public class NormalAttackVisitor : IAttackVisitor
    {

        private Player _player;

        public override string Name => "CLASSIC ATTACK";
        public NormalAttackVisitor(Player player)
        {
            _player = player;
        }

        public override void Visit(HeavyWeapon core, Items.Item context)
        {
            CalculatedDamageToEnemy = context.Damage;
            CalculatedPlayerDefense = _player.Stats.Strength + _player.Stats.Luck;
        }

        public override void Visit(LightWeapon core, Items.Item context)
        {
            CalculatedDamageToEnemy = context.Damage;
            CalculatedPlayerDefense = _player.Stats.Agility + _player.Stats.Luck;
        }

        public override void Visit(MagicWeapon core, Items.Item context)
        {
            CalculatedDamageToEnemy = 1;
            CalculatedPlayerDefense = _player.Stats.Agility + _player.Stats.Luck;
        }

        public override void Visit(Items.Item core, Items.Item context)
        {
            CalculatedDamageToEnemy = 0;
            CalculatedPlayerDefense = _player.Stats.Agility;
        }
    }
}
