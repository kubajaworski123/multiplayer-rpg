using MazeGame.Model.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Map;

public class Map
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Tile[,] Tiles { get; private set; }
    public void AddEntityToTile(MapEntity entity, int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height && !Tiles[x,y].IsWall)
        {
            Tiles[x, y].AddMapEntity(entity);
        }
    }
    public void RemoveEntityFromTile(MapEntity entity, int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            Tiles[x, y].RemoveMapEntity(entity);
        }
    }
    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Tiles = new Tile[Width, Height];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if( x == 0 || x == Width - 1 || y == 0 || y == Height - 1 || (x == 10 && y != 10))
                {
                    Tiles[x, y] = new Tile('█', true);
                }
                else
                {
                    Tiles[x, y] = new Tile(' ', false);
                }
                
            }
        }
        Tiles[2, 2].AddMapEntity(new OneHandedWeapon("Short Sword", 's', 2));
        Tiles[3, 5].AddMapEntity(new OneHandedWeapon("Short Axe", 'a', 10));
        Tiles[19, 12].AddMapEntity(new TwoHandedWeapon("Great Sword", 'S', 69));
        Tiles[5, 3].AddMapEntity(new Item("key", 'k'));
        Tiles[30, 12].AddMapEntity(new Item("collectible", 'c'));
        Tiles[20, 15].AddMapEntity(new Item("junk", 'j'));
        Tiles[35, 16].AddMapEntity(new Gold("gold", 'G', 10));
        Tiles[8, 6].AddMapEntity(new Money("money", 'M', 100));
    }
}