using MazeGame.Model.Items;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
namespace MazeGame.Model.Map;
public class ConcreteMapBuilder : IMapBuilder
{
    private Map _map;
    private List<Chamber> _chambers = new List<Chamber>();
    private Random _random = new Random();
    private const int MAX_TRIES = 1000;
    public void BuildEmptyDungeon(int width, int height)
    {
        _map = new Map(width, height, false);
        _chambers.Add(new Chamber(1,1,1,1));
    }
    public void BuildFilledDungeon(int width, int height)
    {
        _map = new Map(width, height, true);
        _chambers.Add(new Chamber(1, 1, 1, 1));
    }
    public void BuildCorridors(int turns)
    {
        List<Chamber> pointsToConnect = new List<Chamber>();

        if (_chambers.Count > 0)
        {
            pointsToConnect.AddRange(_chambers);
        }

        for (int i = 0; i < turns; i++)
        {
            int fPx = _random.Next(1, _map.Width - 1);
            int fPy = _random.Next(1, _map.Height - 1);
            pointsToConnect.Add(new Chamber(fPx, fPy, 1, 1));
        }

        if (pointsToConnect.Count < 2) return;

        List<Chamber> unconnected = new List<Chamber>(pointsToConnect);
        List<Chamber> connected = new List<Chamber>();

        Chamber? current = unconnected[0];
        unconnected.Remove(current);
        connected.Add(current);

        while (unconnected.Count > 0)
        {
            Chamber? nearest = null;
            double minDistance = double.MaxValue;

            foreach (var candidate in unconnected)
            {
                double dx = current.CX - candidate.CX;
                double dy = current.CY - candidate.CY;
                double distanceSquared = (dx * dx) + (dy * dy);

                if (distanceSquared < minDistance)
                {
                    minDistance = distanceSquared;
                    nearest = candidate;
                }
            }

            current = nearest;
            unconnected.Remove(current);
            connected.Add(current);
        }

        for (int i = 0; i < connected.Count - 1; i++)
        {
            int r = _random.Next(0, 2);
            if (r == 0)
            {
                BuildHorizontalTunnel(connected[i].CX, connected[i].CY, connected[i + 1].CX);
                BuildVerticalTunnel(connected[i + 1].CX, connected[i].CY, connected[i + 1].CY);
            }
            else
            {
                BuildVerticalTunnel(connected[i].CX, connected[i].CY, connected[i + 1].CY);
                BuildHorizontalTunnel(connected[i].CX, connected[i + 1].CY, connected[i + 1].CX);
            }
        }
    }
    public void BuildChambers(int N)
    {
        int tries = 0;
        for(int i = 0; i < N; i++)
        {
            if (tries > MAX_TRIES)
                return;
            int rX = _random.Next(1, _map.Width - Chamber.MaxW);
            int rY = _random.Next(1, _map.Height - Chamber.MaxH);
            int rW = _random.Next(2, Chamber.MaxW + 1);
            int rH = _random.Next(2, Chamber.MaxH + 1);

            bool foundfreespace = true;
            for (int x = rX-1; x < rX + rW + 1; x++)
            {
                for (int y = rY - 1; y < rY + rH + 1; y++)
                {
                    if(!_map.Tiles[x, y].IsWall)
                    {
                        foundfreespace = false; break;
                    }
                }
            }
            if (foundfreespace)
            {
                _chambers.Add(new Chamber(rX, rY, rW, rH));
                for (int x = rX; x < rX + rW; x++)
                {
                    for (int y = rY; y < rY + rH; y++)
                    {
                        _map.Tiles[x, y].Dig();
                    }
                }
            }
            else
            {
                i--;
                tries++;
            }
        }
    }
    public void BuildCentralHall(int width, int height)
    {
        int cX = (_map.Width  - width) / 2;
        int cY = (_map.Height - height) / 2;
        _chambers.Add(new Chamber(cX, cY, width, height));
        for (int x = cX; x < cX + width; x++)
        {
            for (int y = cY; y < cY + height; y++)
            {
                _map.Tiles[x, y].Dig();
            }
        }

    }
    public void BuildItems(int count)
    {
        var nonWall = _map.GetNonWallTiles();
        int itemsN = Item.Items.Count;
        int nonWallN = nonWall.Count;
        for (int i = 0; i < count; i++)
        {
            int rItem = _random.Next(0,itemsN);
            int rTile = _random.Next(0,nonWallN);
            nonWall[rTile].AddMapEntity(Item.Items[rItem]);
        }
    }
    public void BuildWeapons(int count)
    {
        var nonWall = _map.GetNonWallTiles();
        int itemsN = OneHandedWeapon.Weapons.Count;
        int nonWallN = nonWall.Count;
        for (int i = 0; i < count; i++)
        {
            int rItem = _random.Next(0, itemsN);
            int rTile = _random.Next(0, nonWallN);
            nonWall[rTile].AddMapEntity(OneHandedWeapon.Weapons[rItem]);
        }
    }
    public Map GetMap()
    {
        return _map;
    }

    private void BuildVerticalTunnel(int startX, int startY, int endY)
    {
        int min = Math.Min(startY, endY);
        int max = Math.Max(startY, endY);
        for (int y = min; y <= max; y++)
        {
            _map.Tiles[startX, y].Dig();
        }
    }
    private void BuildHorizontalTunnel(int startX, int startY, int endX)
    {
        int min = Math.Min(startX, endX);
        int max = Math.Max(startX, endX);

        for (int x = min; x <= max; x++)
        {
            _map.Tiles[x, startY].Dig();
        }
    }

}