using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items;

public class TwoHandedWeapon(string name, char symbol, int damage): Weapon(name, symbol, damage)
{
    public override void Equip(Player player, Map.Map map)
    {
        if (player.LeftHand != null && player.LeftHand == player.RightHand)
        {
            if (player.Inventory.Count < player.InventoryCapacity)
            {
                player.AddToInventory(player.LeftHand);
            }
            else
            {
                player.LeftHand.Drop(player, map);
            }
            player.LeftHand = null;
            player.RightHand = null;
        }
        else
        {
            if (player.LeftHand != null)
            {
                if (player.Inventory.Count < player.InventoryCapacity)
                {
                    player.AddToInventory(player.LeftHand);
                }
                else
                {
                    player.LeftHand.Drop(player, map);
                }
                player.LeftHand = null;
            }
            if (player.RightHand != null)
            {
                if (player.Inventory.Count < player.InventoryCapacity)
                {
                    player.AddToInventory(player.RightHand);
                }
                else
                {
                    player.RightHand.Drop(player, map);
                }
                player.RightHand = null;
            }
        }
        player.LeftHand = this;
        player.RightHand = this;
        player.RemoveFromInventory(this);
    }
}