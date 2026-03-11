using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items;

public abstract class Weapon(string name, char symbol, int damage = 0) : Item(name, symbol)
{
    public int Damage { get; protected set; } = damage;
    public void Use() { }

    public override string ToString()
    {
        return $"{Name} (Damage: {Damage})";
    }

}
