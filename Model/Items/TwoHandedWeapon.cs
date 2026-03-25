using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items;

public class TwoHandedWeapon(string name, char symbol, int damage): Weapon(name, symbol, damage)
{
    public static List<TwoHandedWeapon> Weapons { get; private set; } = new List<TwoHandedWeapon>()
    {
        new TwoHandedWeapon("Wooden Club", 'C', 2),
        new TwoHandedWeapon("Rusty Greatsword", 'R', 4),
        new TwoHandedWeapon("Iron Halberd", 'H', 6),
        new TwoHandedWeapon("Heavy Maul", 'M', 8),
        new TwoHandedWeapon("Steel Pike", 'P', 9),
        new TwoHandedWeapon("Battle Axe", 'A', 10),
        new TwoHandedWeapon("Glaive", 'G', 11),
        new TwoHandedWeapon("Zweihander", 'Z', 12),
        new TwoHandedWeapon("Warhammer", 'W', 13),
        new TwoHandedWeapon("Executioner Sword", 'E', 14),
        new TwoHandedWeapon("Bardiche", 'B', 12),
        new TwoHandedWeapon("Scythe", 'S', 10),
        new TwoHandedWeapon("Lucerne Hammer", 'L', 11),
        new TwoHandedWeapon("Titanium Claymore", 'T', 15),
        new TwoHandedWeapon("Flamberge", 'F', 16),
        new TwoHandedWeapon("Demon Trident", 'D', 17),
        new TwoHandedWeapon("Kraken Anchor", 'K', 16),
        new TwoHandedWeapon("Vampiric Odachi", 'O', 18),
        new TwoHandedWeapon("Meteor Greataxe", 'X', 19),
        new TwoHandedWeapon("Vorpal Blade", 'V', 20)
    };
    public override void Equip(Player player, Map.Map map)
    {
        player.hasWeapons = true;

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