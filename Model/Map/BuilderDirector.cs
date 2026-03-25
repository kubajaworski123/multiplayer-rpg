using MazeGame.Controller;
using MazeGame.Model.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Map;

public static class BuilderDirector
{
    public static readonly Dictionary<int, Action<IMapBuilder>> MenuOptionsMap = new Dictionary<int, Action<IMapBuilder>>()
        {
        {0, Full },
        {1, NoCenHall },
        {2, LessRooms },
        {3, LongCorridors },
        {4, OpenWorld },
        {5, NoItems }
        };

    public static (Map, List<string>, List<string>, List<string>) ProcessMapChoice(int menuOption)
    {
        Action<IMapBuilder> action = MenuOptionsMap[menuOption];
        var MB = new ConcreteMapBuilder();
        var CL = new ControlsListBuilder();
        var SWS = new StarWarsSubtitlesBuilder();
        var UM = new UsersManualBuilder();
        action(MB); action(CL); action(SWS); action(UM);
        return(MB.GetMap(), CL.GetControls(), SWS.GetManual(), UM.getUsersManual());
    }
    public static void Full(IMapBuilder mapBuilder)
    {
        mapBuilder.BuildFilledDungeon(40, 20);
        mapBuilder.BuildCentralHall(10, 5);
        mapBuilder.BuildChambers(150000);
        mapBuilder.BuildCorridors(0);
        mapBuilder.BuildItems(5);
        mapBuilder.BuildWeapons(20);
        
    }
    public static void NoCenHall(IMapBuilder mapBuilder)
    {
        mapBuilder.BuildFilledDungeon(40, 20);
        mapBuilder.BuildChambers(150000);
        mapBuilder.BuildCorridors(0);
        mapBuilder.BuildItems(5);
        mapBuilder.BuildWeapons(20);
    }
    public static void OpenWorld(IMapBuilder mapBuilder)
    {
        mapBuilder.BuildEmptyDungeon(40, 20);
        mapBuilder.BuildChambers(4);
        mapBuilder.BuildCorridors(0);
        mapBuilder.BuildItems(5);
        mapBuilder.BuildWeapons(10);
    }
    public static void NoItems(IMapBuilder mapBuilder) 
    { 
        mapBuilder.BuildFilledDungeon(40, 20);
        mapBuilder.BuildCentralHall(10, 5);
        mapBuilder.BuildChambers(150000);
        mapBuilder.BuildCorridors(0);
    }
    public static void LongCorridors(IMapBuilder mapBuilder)
    {
        mapBuilder.BuildFilledDungeon(40, 20);
        mapBuilder.BuildChambers(3);
        mapBuilder.BuildCorridors(8);
        mapBuilder.BuildItems(5);
        mapBuilder.BuildWeapons(10);
    }

    public static void LessRooms(IMapBuilder mapBuilder)
    {
        mapBuilder.BuildFilledDungeon(40, 20);
        mapBuilder.BuildChambers(4);
        mapBuilder.BuildCentralHall(10, 5);
        mapBuilder.BuildCorridors(0);
        mapBuilder.BuildItems(5);
        mapBuilder.BuildWeapons(10);
    }

}
