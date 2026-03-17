using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Model.Map;
using MazeGame.View;

namespace MazeGame.Model.Items;

public class Item(string name, char symbol) : MapEntity(name, symbol)
{
    public static List<Item> Items { get; private set; } = new List<Item>()
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
    public override void PickUp(Player player)
    {
        player.AddToInventory(this);
    }
    public virtual void Equip(Player player, Map.Map map) 
    {
        Logger.SingletonLogger.Add($"You can't equip {Name}.");
    }

    public virtual void Drop(Player player, Map.Map map) 
    {
        int x = player.X;
        int y = player.Y;
        map.Tiles[x, y].AddMapEntity(this);
        player.RemoveFromInventory(this);
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}
