using MazeGame.Model.Characters;
using MazeGame.Model.Dungeon;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Combat
{
    public interface ICombat
    {
        int Health { get; }
        string Name { get; }
        CharacterType Type { get; }
        int X { get; }
        int Y { get; }
        char Symbol { get; }
        void ReciveDamage(int damage);
        void Attack(ICombat enemy);
        void SetType(CharacterType type);
        void Move(Map map, int x, int y);
    }
}
