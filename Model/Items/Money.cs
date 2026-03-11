using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items;

public class Money(string name, char symbol, int amount) : Currency(name, symbol, amount)
{
    public override void PickUp(Player player)
    {
        player.ModifyMoney(Amount);
    }
}