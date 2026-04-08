using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Combat
{
    public interface ICombat
    {
        void ReciveDamage(int damage);
        void Attack(ICombat enemy);
    }
}
