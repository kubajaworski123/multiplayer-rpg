using MazeGame.Model.Characters;
using MazeGame.Model.Combat;
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
    public List<ICombat> Combatants { get; private set; } = new List<ICombat>();
    public void AddCombatant(ICombat combatant)
    {
        Combatants.Add(combatant);
    }
    public void RemoveCombatant(ICombat combatant)
    {
        Combatants.Remove(combatant);
    }
    public List<ICombat> GetCombatantsFromTile(int x, int y)
    {
        List<ICombat> combatantsOnTile = new List<ICombat>();
        foreach (ICombat combatant in Combatants)
        {
            if (combatant.X == x && combatant.Y == y)
            {
                combatantsOnTile.Add(combatant);
            }
        }
        return combatantsOnTile;
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
    public void MoveMobs(int playerX, int playerY)
    {
        foreach (var combatant in Combatants.ToList())
        {
            combatant.Move(this, playerX, playerY);
        }
    }
    public int GetPathDistance(int startX, int startY, int targetX, int targetY)
    {
        if (startX == targetX && startY == targetY) return 0;

        bool[,] visited = new bool[Width, Height];
        Queue<(int x, int y, int dist)> queue = new Queue<(int x, int y, int dist)>();

        queue.Enqueue((startX, startY, 0));
        visited[startX, startY] = true;

        (int x, int y)[] directions = new (int x, int y)[]
        {
            (0, -1), 
            (0, 1),  
            (-1, 0), 
            (1, 0)   
        };

        while (queue.Count > 0)
        {
            var curr = queue.Dequeue();

            if (curr.x == targetX && curr.y == targetY)
                return curr.dist;

            for (int i = 0; i < 4; i++)
            {
                int nextX = curr.x + directions[i].x;
                int nextY = curr.y + directions[i].y;

                if (nextX >= 0 && nextX < Width && nextY >= 0 && nextY < Height)
                {
                    if (!visited[nextX, nextY] && !Tiles[nextX, nextY].IsWall)
                    {
                        visited[nextX, nextY] = true;
                        queue.Enqueue((nextX, nextY, curr.dist + 1));
                    }
                }
            }
        }
        return -1;
    }

}