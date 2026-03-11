using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MazeGame.Model.Items;
public class Player
{
    public char Symbol { get; private set; } = '¶';
    public int Strength { get; private set; } = 1;
    public int Agility { get; private set; } = 1;
    public int Wisdom { get; private set; } = 1;
    public int Health { get; private set; } = 100;
    public int Happiness { get; private set; } = 3;
    public int Agressiveness { get; private set; } = 1;
    public int InventoryCapacity { get; private set; } = 10;
    public int Gold { get; private set; } = 0;
    public int Money { get; private set; } = 0;
    public List<Item> Inventory { get; private set; } = new List<Item>();
    public Weapon? LeftHand { get; set; }
    public Weapon? RightHand { get; set; }
    public int X { get; set; } = 1;
    public int Y { get; set; } = 1;

    public void ModifyStrength(int amount)
    {
        Strength += amount;
        if (Strength < 0) Strength = 0;
    }
    public void ModifyAgility(int amount) {
        Agility += amount;
        if (Agility < 0) Agility = 0;
    }
    public void ModifyWisdom(int amount) {
        Wisdom += amount;
        if (Wisdom < 0) Wisdom = 0;
    }
    public void ModifyHealth(int amount) {
        Health += amount;
        if (Health < 0) Health = 0;
    }
    public void ModifyHappiness(int amount) {
        Happiness += amount;
        if (Happiness < 0) Happiness = 0;
    }
    public void ModifyAgressiveness(int amount) {
        Agressiveness += amount;
        if (Agressiveness < 0) Agressiveness = 0;
    }
    public void ModifyGold(int amount) { Gold += amount; }
    public void ModifyMoney(int amount) { Money += amount; }

    public void AddToInventory(Item item)
    {
        if (Inventory.Count < InventoryCapacity)
        {
            Inventory.Add(item);
        }
    }
    public void RemoveFromInventory(Item item)
    {
        Inventory.Remove(item);
    }

}
