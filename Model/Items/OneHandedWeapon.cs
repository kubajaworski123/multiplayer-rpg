using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items;

public class OneHandedWeapon(string name, char symbol, int damage) : Weapon(name, symbol, damage) 
{
    public static List<OneHandedWeapon> Weapons { get; private set; } = new List<OneHandedWeapon>()
    {
        new OneHandedWeapon("Short Sword", 's', 2),
        new OneHandedWeapon("Broken Blade", 'b', 0),
        new OneHandedWeapon("Rusty Dagger", 'r', 1),
        new OneHandedWeapon("Iron Cleaver", 'i', 2),
        new OneHandedWeapon("Bronze Mace", 'm', 3),
        new OneHandedWeapon("Venomous Fang", 'v', 4),
        new OneHandedWeapon("Steel Broadsword", 's', 5),
        new OneHandedWeapon("Obsidian Dagger", 'o', 4),
        new OneHandedWeapon("Silver Rapier", 'r', 6),
        new OneHandedWeapon("Golden Sickle", 'g', 2),
        new OneHandedWeapon("Dwarven Axe", 'd', 7),
        new OneHandedWeapon("Elven Shortsword", 'e', 5),
        new OneHandedWeapon("Orcish Chopper", 'c', 6),
        new OneHandedWeapon("Bone Club", 'B', 3),
        new OneHandedWeapon("Crystal Blade", 'C', 8),
        new OneHandedWeapon("Flaming Scimitar", 'f', 7),
        new OneHandedWeapon("Frost Hatchet", 'h', 5),
        new OneHandedWeapon("Shadow Gladius", 's', 8),
        new OneHandedWeapon("Thunder Flail", 't', 9),
        new OneHandedWeapon("Dragon Tooth", 'd', 10),
        new OneHandedWeapon("Void Blade", 'b', 10),
    };
    public override void Equip(Player player, Map.Map map)
    {
        player.hasWeapons = true;

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