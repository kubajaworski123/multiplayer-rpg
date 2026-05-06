using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Model.Dungeon;
using MazeGame.View;
using MazeGame.Model.Combat;
using MazeGame.Model.MapEntities;
using MazeGame.Model.Players;

namespace MazeGame.Model.Items;

public class Item(string name, char symbol) : MapEntity(name, symbol)
{
    public virtual int Damage => 0;
    public virtual bool IsTwoHanded => false;
    public virtual ItemRarity Rarity { get; set; } = ItemRarity.Common;
    public override int NoiseRange => 1;
    public virtual void Accept(IAttackVisitor visitor, Item context)
    {
        visitor.Visit(this, context);
    }
    public override bool PickUp(Player player)
    {
        bool result = player.Backpack.AddToInventory(this);
        return result;
    }
    public virtual void Equip(Player player, Map map) 
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
            if (player.Backpack.Inventory.Count < player.Backpack.InventoryCapacity)
            {
                player.Backpack.AddToInventory(player.LeftHand);
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
            if (player.Backpack.Inventory.Count < player.Backpack.InventoryCapacity)
            {
                player.Backpack.AddToInventory(player.LeftHand);
                player.LeftHand = this;
            }
            else
            {
                player.LeftHand.Drop(player, map);
                player.LeftHand = this;
            }
        }
    }

    public virtual void Drop(Player player, Map map) 
    {
        int x = player.X;
        int y = player.Y;
        map.Tiles[x, y].AddMapEntity(this);
        player.Backpack.RemoveFromInventory(this);
    }

    public override string ToString()
    {
        return $"{Name} ({Damage})";
    }
}
