using MazeGame.Model.Items;
using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Characters.PlayerComponents
{
    public class Backpack(int cap = 10, int g = 0, int m = 0)
    {
        public int InventoryCapacity { get; private set; } = cap;
        public int Gold { get; private set; } = g;  
        public int Money { get; private set; } = m;
        public List<Item> Inventory { get; private set; } = new List<Item>();
        public void ModifyGold(int amount) { Gold += amount; }
        public void ModifyMoney(int amount) { Money += amount; }

        public bool AddToInventory(Item item)
        {
            if (Inventory.Count < InventoryCapacity)
            {
                Inventory.Add(item);
                return true;
            }
            return false;
        }
        public void RemoveFromInventory(Item item)
        {
            Inventory.Remove(item);
        }
    }
}
