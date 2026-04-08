using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Characters
{
    public abstract class Character(string name = "unknown", char symbol = '!', int health = 100, int x = 1, int y = 1, 
                                    List<string>? banner = null)
    {
        public string Name { get; protected set; } = name;
        public char Symbol { get; protected set; } = symbol;
        public int Health { get; protected set; } = health;
        public List<string>? Banner { get; protected set; } = banner;
        public virtual string ColorCode => "\x1b[37m";
        public int X { get;  set; } = x;
        public int Y { get;  set; } = y;
        public void ModifyHealth(int amount)
        {
            Health += amount;
            if (Health < 0) Health = 0;
        }
        public void SetBanner(List<string> newBanner)
        {
            Banner = newBanner;
        }
        public void Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}