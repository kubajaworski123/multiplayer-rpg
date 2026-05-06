using System;
using MazeGame.Model.Observers;

namespace MazeGame.Model.Characters
{
    public class HumanMob : Mob
    {
        public HumanMob(int x = 1, int y = 1, IMobPublisher? mobGroup = null, string name = "Human", char symbol = 'H', int health = 100,
               CharacterType characterType = CharacterType.BountyHunter, int attack = 5, int defense = 2)
               : base(name, symbol, health, x, y, characterType, attack, defense, mobGroup)
        {
        }

        public override void OnFriendsDeath()
        {
            if (Damage > 1)
            {
                Damage -= 2;
            }
        }
        public void SetMobGroup(IMobPublisher mobGroup)
        {
            _mobGroup = mobGroup;
            _mobGroup.Subscribe(this);
        }
    }
}