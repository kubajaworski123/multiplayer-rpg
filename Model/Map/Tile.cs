using MazeGame.Model.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace MazeGame.Model.Map;

public class Tile(char symbol = ' ', bool isWall = false)
{
    public char Symbol { get; private set; } = symbol;
    public bool IsWall { get; private set; } = isWall;
    public List<MapEntity> MapEntities { get; private set; } = new List<MapEntity>();
    public void AddMapEntity(MapEntity entity)
    {
        MapEntities.Add(entity);
        UpdateSymbol();
    }
    public void RemoveMapEntity(MapEntity entity)
    {
            MapEntities.Remove(entity);
            UpdateSymbol();
    }
    public char GetSymbol()
    {
        if (IsWall)
        {
            return '█';
        }
        else
        {
            if (MapEntities.Count == 1)
            {
                return MapEntities[0].Symbol;
            }
            else if (MapEntities.Count > 1 && MapEntities.Count < 10)
            {
                return MapEntities.Count.ToString()[0];
            }
            else if (MapEntities.Count >= 10)
            {
                return '+';
            }
            else
            {
                return ' ';
            }
        }
    }
    public void UpdateSymbol()
    {
        char newSymbol = GetSymbol();
        Symbol = newSymbol;
    }
}
