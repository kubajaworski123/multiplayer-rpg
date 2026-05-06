using System;
using MazeGame.Model.Observers;

namespace MazeGame.Model.Characters
{
    public class MachineMob : Mob
    {
        public MachineMob(int x = 1, int y = 1, IMobPublisher? mobGroup = null, string name = "Machine", char symbol = 'M', int health = 100,
               CharacterType characterType = CharacterType.Machine, int attack = 6, int defense = 3)
               : base(name, symbol, health, x, y, characterType, attack, defense, mobGroup)
        {
        }

        public override void OnFriendsDeath()
        {
            Damage += 2;
        }
    }
}