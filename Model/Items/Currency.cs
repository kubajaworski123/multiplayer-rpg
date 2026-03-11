using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items;

public abstract class Currency(string name, char symbol, int amount = 1) : MapEntity(name, symbol)
{
    public int Amount { get; protected set; } = amount;

    public override string ToString()
    {
        return $"{Name} ({Amount})";
    }
}
