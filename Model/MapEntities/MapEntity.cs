using MazeGame.Model.Items;
using MazeGame.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;
namespace MazeGame.Model.MapEntities;
public abstract class MapEntity(string name, char symbol)
{
    public virtual string Name { get; protected set; } = name;
    public char Symbol { get; protected set; } = symbol;

    public abstract bool PickUp(Player player);
    public virtual int NoiseRange => 0;

    public virtual IEnumerable<Item> GetItemsOnTile()
    {
        yield break;
    }
}
