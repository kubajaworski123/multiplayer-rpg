using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Map;

public class BuilderDirector
{
    public static void Full(IMapBuilder mapBuilder)
    {
        mapBuilder.BuildFilledDungeon(40, 20);
        mapBuilder.BuildCentralHall(10, 5);
        mapBuilder.BuildChambers(150000);
        mapBuilder.BuildCorridors(0);
        mapBuilder.BuildItems(5);
        mapBuilder.BuildWeapons(20);
    }
    public static void NoItems(IMapBuilder mapBuilder)
    {
        mapBuilder.BuildFilledDungeon(40, 20);
        mapBuilder.BuildCentralHall(10, 5);
        mapBuilder.BuildChambers(150000);
        mapBuilder.BuildCorridors(0);
    }
}
