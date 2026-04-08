using MazeGame.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.MapEntities.Currencies;
public class Gold(string name, char symbol, int amount) : Currency(name, symbol, amount)
{
    public override void PickUp(Player player)
    {
        player.Backpack.ModifyGold(Amount);
    }
}
