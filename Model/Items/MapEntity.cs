using System;
using System.Collections.Generic;
using System.Text;
namespace MazeGame.Model.Items;
public abstract class MapEntity(string name, char symbol)
{
    public string Name { get; protected set; } = name;
    public char Symbol { get; protected set; } = symbol;

    public abstract void PickUp(Player player);

}
