using MazeGame.Controller;
using MazeGame.Model;
using MazeGame.Model.Items;
using MazeGame.Model.Map;
using MazeGame.View;
using System;

namespace MazeGame.Controller;

public class MainController
{
    private readonly Player _player;
    private readonly Map _map;
    private readonly FrameRenderer _renderer;

    private InputHandler? _inputChain;
    private bool _isRunning;

    public MainController(Player player, Map map, FrameRenderer renderer)
    {
        _player = player;
        _map = map;
        _renderer = renderer;
        _isRunning = true;
        InitializeInputChain();
    }

    private void InitializeInputChain()
    {
        Action refreshView = () =>
        {
            _renderer.GetNewFrame(_map, _player);
            _renderer.RenderFrame();
        };

        Action StopRunning = () =>
        {
            _isRunning = false;
        };

        Action Pickup = () =>
        {
            MapEntity item = _map.Tiles[_player.X, _player.Y].MapEntities.First();
            item.PickUp(_player);
            _map.Tiles[_player.X, _player.Y].RemoveMapEntity(item);
            _renderer.GetNewFrame(_map, _player);
            _renderer.RenderFrame();
        };

        Action<int> QuickEquip = (x) =>
        {
            if (_player.Inventory.Count > x)
            {
                Item item = _player.Inventory[x];
                item.Equip(_player, _map);
                _renderer.GetNewFrame(_map, _player);
                _renderer.RenderFrame();
            }
        };

        Action <int> QuickDrop = (x) =>
        {
            if (_player.Inventory.Count > x)
            {
                Item item = _player.Inventory[x];
                item.Drop(_player, _map);
                _renderer.GetNewFrame(_map, _player);
                _renderer.RenderFrame();
            }
        };

        var systemHandler = new SystemActionHandler(StopRunning);
        var movementHandler = new MovementHandler(_player, _map, refreshView);
        var pickupHandler = new PickupHandler(_player, _map, Pickup);
        var equipHandler = new EquipHandler(_player, QuickEquip);
        var dropHandler = new DropHandler(_player, QuickDrop);

        systemHandler.SetNextHandler(movementHandler);
        movementHandler.SetNextHandler(pickupHandler);
        pickupHandler.SetNextHandler(equipHandler);
        equipHandler.SetNextHandler(dropHandler);

        _inputChain = systemHandler;
    }

    public void Run()
    {
        _renderer.GetNewFrame(_map, _player);
        _renderer.RenderFrame();

        while (_isRunning)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                _inputChain?.Handle(keyInfo);
            }
        }
        Console.Clear();
    }
}