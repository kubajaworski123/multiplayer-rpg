using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Characters
{
    public abstract class Character(string name = "unknown", char symbol = '!', int health = 100,
                                    CharacterType type = CharacterType.BountyHunter, int x = 1, int y = 1)
    {
        public string Name { get; protected set; } = name;
        public char Symbol { get; protected set; } = symbol;
        public int Health { get; protected set; } = health;
        public virtual CharacterType Type { get; protected set; } = type;
        public int X { get;  set; } = x;
        public int Y { get;  set; } = y;
        public virtual void ModifyHealth(int amount)
        {
            Health += amount;
            if (Health < 0) Health = 0;
        }
        public void SetType(CharacterType type)
        {
            Type = type;
        }
        public void Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}