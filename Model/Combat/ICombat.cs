using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Combat
{
    public interface ICombat
    {
        int Health { get; }
        string Name { get; }
        List<string> Banner { get; }
        int X { get; }
        int Y { get; }
        char Symbol { get; }
        string ColorCode { get; }
        void ReciveDamage(int damage);
        void Attack(ICombat enemy);
        void SetBanner(List<string> newBanner);
    }
}
