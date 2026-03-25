using MazeGame.Model;
using MazeGame.Model.Items;
using MazeGame.Model.Map;
using MazeGame.View;
using System;

namespace MazeGame.Controller.MainMapHandlers;

public class MovementHandler(Player player, Map map, FrameRenderer renderer) : InputHandler
{
    private readonly Player _player = player;
    private readonly Map _map = map;
    private readonly FrameRenderer _renderer = renderer;

    public void Refresh()
    {
        _renderer.GetNewFrame();
        _renderer.RenderFrame();
    }
    public override void Handle(ConsoleKeyInfo keyInfo)
    {
        int newX = _player.X;
        int newY = _player.Y;
        bool isMovementKey = true;
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.W:
                newY--;
                break;
            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                newY++;
                break;
            case ConsoleKey.LeftArrow:
            case ConsoleKey.A:
                newX--;
                break;
            case ConsoleKey.RightArrow:
            case ConsoleKey.D:
                newX++;
                break;
            default:
                isMovementKey = false;
                break;
        }

        if (isMovementKey)
        {
            if (CanMoveTo(newX, newY))
            {
                _player.X = newX;
                _player.Y = newY;
                Refresh();
            }
        }
        else
        {
            base.Handle(keyInfo);
        }
    }

    private bool CanMoveTo(int x, int y)
    {
        if (x < 0 || x >= _map.Width || y < 0 || y >= _map.Height)
        {
            return false;
        }

        if (_map.Tiles[x, y].IsWall)
        {
            return false;
        }
        return true;
    }
}