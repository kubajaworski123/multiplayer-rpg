using MazeGame.Controller;
using MazeGame.Model.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Map;

public class CORMapBuilder(Player player, Map map, Action onMove, Action onExit, Action onPickup, Action<int> onEquip,
Action<int> onDrop, Action<ConsoleKeyInfo> onUnknownInput) : IMapBuilder
{
    private bool _move         = false;
    private bool _exit         = false;
    private bool _pickup       = false;
    private bool _equip        = false;
    private bool _drop         = false;
    public InputHandler? COR { get; private set; } =  new UnknownInputHandler(onUnknownInput);
    public void BuildCentralHall(int width, int height)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, onMove);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
    }

    public void BuildChambers(int N)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, onMove);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
    }

    public void BuildCorridors(int turns)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, onMove);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
    }

    public void BuildEmptyDungeon(int width, int height)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, onMove);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
        if(!_exit && COR != null)
        {
            var eH = new SystemActionHandler(onExit);
            eH.SetNextHandler(COR);
            COR = eH;
            _exit = true;
        }
    }

    public void BuildFilledDungeon(int width, int height)
    {
        if (!_exit && COR != null)
        {
            var eH = new SystemActionHandler(onExit);
            eH.SetNextHandler(COR);
            COR = eH;
            _exit = true;
        }
    }

    public void BuildItems(int count)
    {
        if(! _pickup && COR != null)
        {
            var pH = new PickupHandler(player, map, onPickup);
            pH.SetNextHandler(COR);
            COR = pH;
            _pickup = true;
        }
        if (!_drop && COR != null)
        {
            var dH = new DropHandler(player, onDrop);
            dH.SetNextHandler(COR);
            COR = dH;
            _drop = true;
        }
    }

    public void BuildWeapons(int count)
    {
        if (!_pickup && COR != null)
        {
            var pH = new PickupHandler(player, map, onPickup);
            pH.SetNextHandler(COR);
            COR = pH;
            _pickup = true;
        }
        if(! _equip  && COR != null)
        {
            var eqH = new EquipHandler(player,onEquip);
            eqH.SetNextHandler(COR);
            COR = eqH;
            _equip = true;
        }
        if(! _drop && COR != null)
        {
            var dH = new DropHandler(player,onDrop);
            dH.SetNextHandler(COR);
            COR = dH;
            _drop = true;
        }
    }
}
