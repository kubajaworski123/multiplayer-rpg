using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items;

public class OneHandedWeapon(string name, char symbol, int damage) : Weapon(name, symbol, damage) 
{
    public override void Equip(Player player, Map.Map map)
    {
        if (player.LeftHand == null)
        {
            player.LeftHand = this;
        }
        else if (player.RightHand == null)
        {
            player.RightHand = this;
        }
        else if (player.LeftHand == player.RightHand)
        {
            if (player.Inventory.Count < player.InventoryCapacity)
            {
                player.AddToInventory(player.LeftHand);
                player.LeftHand = this;
                player.RightHand = null;
            }
            else
            {
                player.RightHand.Drop(player, map);
                player.LeftHand = this;
                player.RightHand = null;
            }
        }
        else
        {
            if (player.Inventory.Count < player.InventoryCapacity)
            {
                player.AddToInventory(player.LeftHand);
                player.LeftHand = this;
            }
            else
            {
                player.LeftHand.Drop(player, map);
                player.LeftHand = this;
            }
        }
        player.RemoveFromInventory(this);
    }

}