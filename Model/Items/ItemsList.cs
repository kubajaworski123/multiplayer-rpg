using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items
{
    internal static class ItemsList
    {
        public static List<Item> Items = new List<Item>()
        {
            new Item("Rusty Key", 'k'),
            new Item("Golden Skeleton Key", 'K'),
            new Item("Mysterious Scroll", 's'),
            new Item("Broken Compass", 'c'),
            new Item("Silver Chalice", 'C'),
            new Item("Dusty Tome", 't'),
            new Item("Glowing Gem", 'g'),
            new Item("Strange Gear", 'G'),
            new Item("Torn Map", 'm'),
            new Item("Empty Bottle", 'b')
        };
        public static Item GetRandomItem()
        {
            Random random = new Random();
            return Items[random.Next(Items.Count)];
        }
    }
}
