using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Model.Map;
using MazeGame.View;

namespace MazeGame.Model.Items;

public class Item(string name, char symbol) : MapEntity(name, symbol)
{
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
