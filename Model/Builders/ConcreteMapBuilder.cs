using MazeGame.Model.Characters;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Dungeon.Themes;
using MazeGame.Model.Items;
using MazeGame.Model.Weapons;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
namespace MazeGame.Model.Builders;
public class ConcreteMapBuilder(IDungeonThemeFactory theme) : IMapBuilder
{
    private Map _map;
    private List<Chamber> _chambers = new List<Chamber>();
    private Random _random = new Random();
    private const int MAX_TRIES = 1000;
    bool    _startedEmpty = true;
    private readonly IDungeonThemeFactory _theme = theme;
    public void BuildEmptyDungeon(int width, int height)
    {
        _map = new Map(width, height, false);
        for (int x = 0; x < width; x++)
        {
            _map[x,0].Fill();
            _map[x,height-1].Fill();
        }
        for (int y = 0; y < height; y++) 
        {
            _map[0, y].Fill();
            _map[width-1, y].Fill();
        }
        _chambers.Add(new Chamber(1,1,1,1));
        _map[1, 1].SetAsChamber();
    }
    public void BuildFilledDungeon(int width, int height)
    {
        _map = new Map(width, height, true);
        _chambers.Add(new Chamber(1, 1, 1, 1));
        _startedEmpty = false;
    }
    public void BuildCorridors(int turns)
    {
        if (!_startedEmpty)
        {
            List<Chamber> pointsToConnect = new List<Chamber>();

            if (_chambers.Count > 0)
            {
                pointsToConnect.AddRange(_chambers);
            }

            for (int i = 0; i < turns; i++)
            {
                int fPx = _random.Next(2, _map.Width - 2);
                int fPy = _random.Next(2, _map.Height - 2);
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
        else
        {
            foreach (var chamber in _chambers)
            {
                int xTop = _random.Next(chamber.X, chamber.X + chamber.W);
                if (chamber.Y - 2 > 0)
                {
                    _map[xTop, chamber.Y - 1].Dig();
                    _map[xTop, chamber.Y - 2].Dig();
                }
                int xBot = _random.Next(chamber.X, chamber.X + chamber.W);
                if (chamber.Y + chamber.H + 1 < _map.Height - 1)
                {
                    _map[xBot, chamber.Y + chamber.H].Dig();
                    _map[xBot, chamber.Y + chamber.H + 1].Dig();
                }
                int yEast = _random.Next(chamber.Y, chamber.Y + chamber.H);
                if (chamber.X + chamber.W + 1 < _map.Width - 1)
                {
                    _map[chamber.X + chamber.W, yEast].Dig();
                    _map[chamber.X + chamber.W + 1, yEast].Dig();
                }
                int yWest = _random.Next(chamber.Y, chamber.Y + chamber.H);
                if (chamber.X - 2 > 0)
                {
                    _map[chamber.X - 1, yWest].Dig();
                    _map[chamber.X-2,yWest].Dig();
                }
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
                for (int y = rY-1; y < rY + rH + 1 ; y++)
                {
                    if( (_startedEmpty ? (_map[x,y].IsWall || _map[x,y].InChamber) : !_map[x, y].IsWall))
                    {
                        foundfreespace = false; break;
                    }
                }
            }
            if (foundfreespace)
            {
                _chambers.Add(new Chamber(rX, rY, rW, rH));
                if (!_startedEmpty)
                {
                    for (int x = rX; x < rX + rW; x++)
                    {
                        for (int y = rY; y < rY + rH; y++)
                        {
                            _map[x, y].Dig();
                            _map[x, y].SetAsChamber();
                        }
                    }
                }
                else
                {
                    for (int x = rX-1; x < rX + rW + 1; x++)
                    {
                        _map[x,rY-1].Fill();
                        _map[x,rY+rH+1].Fill();
                    }
                    for (int y = rY-1; y <= rY + rH + 1; y++)
                    {
                        _map[rX - 1, y].Fill();
                        _map[rX + rW + 1, y].Fill();
                    }
                    for (int x = rX; x < rX + rW; x++)
                    {
                        for (int y = rY; y < rY + rH; y++)
                        {
                            _map[x, y].SetAsChamber();
                        }
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
        if (!_startedEmpty)
        {
            for (int x = cX; x < cX + width; x++)
            {
                for (int y = cY; y < cY + height; y++)
                {
                    _map.Tiles[x, y].Dig();
                }
            }
        }
        else
        {
            for (int x = cX - 1; x < cX + width + 1; x++)
            {
                _map[x, cY - 1].Fill();
                _map[x, cY + height + 1].Fill();
            }
            for (int y = cY - 1; y <= cY + height + 1; y++)
            {
                _map[cX - 1, y].Fill();
                _map[cX + width + 1, y].Fill();
            }
            for (int x = cX; x < cX + width; x++)
            {
                for (int y = cY; y < cY + height; y++)
                {
                    _map[x,y].Dig();
                    _map[x, y].SetAsChamber();
                }
            }
        }

    }
    public void AddItems(int count)
    {
        var nonWall = _map.GetNonWallTiles();
        int nonWallN = nonWall.Count;
        for (int i = 0; i < count; i++)
        {
            int rTile = _random.Next(0,nonWallN);
            nonWall[rTile].AddMapEntity(_theme.GetRandomItem());
        }
    }
    public void AddWeapons(int count)
    {
        var nonWall = _map.GetNonWallTiles();
        int nonWallN = nonWall.Count;
        for (int i = 0; i < count; i++)
        {
            int rTile = _random.Next(0, nonWallN);
            nonWall[rTile].AddMapEntity(_theme.GetRandomWeapon());
        }
    }
    public void AddMobs(int count)
    {
        //var nonWall = _map.GetNonWallPositions();
        //int nonWallN = nonWall.Count;
        //for (int i = 0; i < count; i++)
        //{
        //    int rTile = _random.Next(0, nonWallN);
        //    (int x, int y) = nonWall[rTile];
        //    Mob mob = _theme.GetRandomMob();
        //    mob.Move(x, y);
        //    _map.AddCombatant(mob);
        //}
    }
    public void AddArtifact()
    {
        var nonWall = _map.GetNonWallTiles();
        if (nonWall.Count > 0)
        {
            int rTile = _random.Next(0, nonWall.Count);
            nonWall[rTile].AddMapEntity(_theme.GetArtifact());
        }
    }
    public void AddSlottedItems()
    {
        var nonWall = _map.GetNonWallTiles();
        if (nonWall.Count == 0) return;

        PlaceOnRandomTile(nonWall, Items.Slots.SlottedItemFactory.CreateSlottedSword());
        PlaceOnRandomTile(nonWall, Items.Slots.SlottedItemFactory.CreateSlottedBlaster());
        PlaceOnRandomTile(nonWall, Items.Slots.SlottedItemFactory.CreateLoadedHandle());
        PlaceOnRandomTile(nonWall, Items.Slots.SlottedItemFactory.CreateLoadedHandle());
        PlaceOnRandomTile(nonWall, Items.Slots.SlottedItemFactory.CreateLuckyStone());
    }
    private void PlaceOnRandomTile(List<Tile> tiles, Item entity)
    {
        tiles[_random.Next(0, tiles.Count)].AddMapEntity(entity);
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
            if (!_startedEmpty)
            {
                _map[startX, y].Dig();
                _map[startX, y].SetAsChamber();
            }
        }
    }

    private void BuildHorizontalTunnel(int startX, int startY, int endX)
    {
        int min = Math.Min(startX, endX);
        int max = Math.Max(startX, endX);

        for (int x = min; x <= max; x++)
        {
            if (!_startedEmpty)
            {
                _map[x, startY].Dig();
                _map[x, startY].SetAsChamber();
            }
        }
    }
    public void AddMobGroup(Func<Mob> mobCreationDelegate, int count)
    {
        var nonWall = _map.GetNonWallPositions();
        int nonWallN = nonWall.Count;
        if (nonWallN == 0) return;

        for (int i = 0; i < count; i++)
        {
            int rTile = _random.Next(0, nonWallN);
            (int x, int y) = nonWall[rTile];
            Mob mob = mobCreationDelegate();
            mob.Move(x, y);
            _map.AddCombatant(mob);
        }
    }

}