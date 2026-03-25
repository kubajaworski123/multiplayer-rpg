using MazeGame.Controller;
using MazeGame.Controller.MainMapHandlers;
using MazeGame.Model.Items;
using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Map;

public class CORMapBuilder(Player player, Map map, FrameRenderer renderer, UsersManualRenderer umRenderer) : IMapBuilder
{
    private bool _move         = false;
    private bool _exit         = false;
    private bool _pickup       = false;
    private bool _equip        = false;
    private bool _drop         = false;

    Player _player = player;
    Map _map = map;
    FrameRenderer _renderer = renderer;
    UsersManualRenderer _umRenderer = umRenderer;
    public InputHandler? COR { get; private set; } =  new UnknownInputHandler(map,player,renderer);
    public void BuildCentralHall(int width, int height)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, renderer);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
    }

    public void BuildChambers(int N)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, renderer);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
    }

    public void BuildCorridors(int turns)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, renderer);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
    }

    public void BuildEmptyDungeon(int width, int height)
    {
        var UMH = new UsersManualHandler(umRenderer);
        UMH.SetNextHandler(COR);
        COR = UMH;
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, renderer);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
        if(!_exit && COR != null)
        {
            var eH = new ExitHandler();
            eH.SetNextHandler(COR);
            COR = eH;
            _exit = true;
        }
    }

    public void BuildFilledDungeon(int width, int height)
    {
        var UMH = new UsersManualHandler(umRenderer);
        UMH.SetNextHandler(COR);
        COR = UMH;
        if (!_exit && COR != null)
        {
            var eH = new ExitHandler();
            eH.SetNextHandler(COR);
            COR = eH;
            _exit = true;
        }
    }

    public void BuildItems(int count)
    {
        if(! _pickup && COR != null)
        {
            var pH = new PickupHandler(player, map, _renderer);
            pH.SetNextHandler(COR);
            COR = pH;
            _pickup = true;
        }
        if (!_drop && COR != null)
        {
            var dH = new DropHandler(player, map, renderer);
            dH.SetNextHandler(COR);
            COR = dH;
            _drop = true;
        }
    }

    public void BuildWeapons(int count)
    {
        if (!_pickup && COR != null)
        {
            var pH = new PickupHandler(player, map, _renderer);
            pH.SetNextHandler(COR);
            COR = pH;
            _pickup = true;
        }
        if(! _equip  && COR != null)
        {
            var eqH = new EquipHandler(player,map,renderer);
            eqH.SetNextHandler(COR);
            COR = eqH;
            _equip = true;
        }
        if(! _drop && COR != null)
        {
            var dH = new DropHandler(player, map, renderer);
            dH.SetNextHandler(COR);
            COR = dH;
            _drop = true;
        }
    }
    public InputHandler? GetCOR()
    {
        return COR;
    }
}
