using MazeGame.Model.Dungeon;
using MazeGame.Model.Items;
using MazeGame.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Weapons;

public abstract class Weapon(string name, char symbol, int damage = 0, bool isTwoHanded = false) : Item(name, symbol)
{
    public override int Damage { get; } = damage;
    public bool IsTwoHanded { get; protected set; } = isTwoHanded;
    public void Use() { }
    public override void Equip(Player player, Map map)
    {
        player.hasWeapons = true;

        if (IsTwoHanded)
        {
            if (player.LeftHand != null && player.LeftHand == player.RightHand)
            {
                if (player.Backpack.Inventory.Count < player.Backpack.InventoryCapacity)
                {
                    player.Backpack.AddToInventory(player.LeftHand);
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
                    if (player.Backpack.Inventory.Count < player.Backpack.InventoryCapacity)
                    {
                        player.Backpack.AddToInventory(player.LeftHand);
                    }
                    else
                    {
                        player.LeftHand.Drop(player, map);
                    }
                    player.LeftHand = null;
                }
                if (player.RightHand != null)
                {
                    if (player.Backpack.Inventory.Count < player.Backpack.InventoryCapacity)
                    {
                        player.Backpack.AddToInventory(player.RightHand);
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
        }
        else
        {
           base.Equip(player, map);
        }

        player.Backpack.RemoveFromInventory(this);
    }
    public override string ToString()
    {
        return $"{Name} ({Damage})";
    }

}
