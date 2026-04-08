using MazeGame.Model.Characters;
using MazeGame.Model.MapEntities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MazeGame.Model.Dungeon;

public class Map
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Tile[,] Tiles { get; private set; }
    public List<Mob> Mobs { get; private set; } = new List<Mob>();
    public void AddMob(Mob mob)
    {
        Mobs.Add(mob);
    }
    public void RemoveMob(Mob mob)
    {
        Mobs.Remove(mob);
    }
    public List<Mob> GetMobsFromTile(int x, int y)
    {
        List<Mob> mobsOnTile = new List<Mob>();
        foreach (Mob mob in Mobs)
        {
            if (mob.X == x && mob.Y == y)
            {
                mobsOnTile.Add(mob);
            }
        }
        return mobsOnTile;
    }
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

    public List<Tile> GetNonWallTiles()
    {
        var nonWallTiles = new List<Tile>();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if(!Tiles[x, y].IsWall)
                    nonWallTiles.Add(Tiles[x, y]);
            }
        }
        return nonWallTiles;
    }
    public List<(int x, int y)> GetNonWallPositions()
    {
        var nonWallPositions = new List<(int x, int y)>();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (!Tiles[x, y].IsWall)
                    nonWallPositions.Add((x, y));
            }
        }
        return nonWallPositions;
    }
    public Map(int width, int height, bool isWall)
    {
        Width = width; Height = height;
        Tiles = new Tile[Width, Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Tiles[i, j] = new Tile(isWall);
            }
        }
    }

    public Tile this[int x, int y]
    {
        get
        {
            if(x >= 0 && x < Width && y < Height && y >= 0)
            {
                return Tiles[x, y];
            }
            else
            {
                throw new IndexOutOfRangeException("map[]");
            }
        }
    }

}