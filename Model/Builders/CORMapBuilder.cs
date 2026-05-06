using MazeGame.Controller;
using MazeGame.Controller.GameStates;
using MazeGame.Controller.MainMapHandlers;
using MazeGame.Model.Characters;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Observers;
using MazeGame.Model.Players;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Builders;

public class CORMapBuilder(Player player, Map map, FrameRenderer renderer,
    GameStateManager stateManager, UserManualState manualState, GameOverState gameOverState, AllLogsState allLogsState) : IMapBuilder
{
    private bool _move         = false;
    private bool _exit         = false;
    private bool _pickup       = false;
    private bool _equip        = false;
    private bool _drop         = false;
    private bool _logs         = false;


    Player _player = player;
    Map _map = map;
    FrameRenderer _renderer = renderer;
    public InputHandler? COR { get; private set; } =  new UnknownInputHandler(map,player);
    public void BuildCentralHall(int width, int height)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, stateManager);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
    }

    public void BuildChambers(int N)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, stateManager);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
    }

    public void BuildCorridors(int turns)
    {
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, stateManager);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
    }

    public void BuildEmptyDungeon(int width, int height)
    {
        var UMH = new UsersManualHandler(stateManager, manualState);
        UMH.SetNextHandler(COR);
        COR = UMH;
        if (!_move && COR != null)
        {
            var mH = new MovementHandler(player, map, stateManager);
            mH.SetNextHandler(COR);
            COR = mH;
            _move = true;
        }
        if(!_exit && COR != null)
        {
            var eH = new ExitHandler(stateManager, gameOverState);
            eH.SetNextHandler(COR);
            COR = eH;
            _exit = true;
        }
        if(!_logs && COR != null)
        {
            var lH = new AllLogsHandler(stateManager, allLogsState);
            lH.SetNextHandler(COR);
            COR = lH;
            _logs = true;
        }
    }

    public void BuildFilledDungeon(int width, int height)
    {
        var UMH = new UsersManualHandler(stateManager, manualState);
        UMH.SetNextHandler(COR);
        COR = UMH;
        if (!_exit && COR != null)
        {
            var eH = new ExitHandler(stateManager,gameOverState);
            eH.SetNextHandler(COR);
            COR = eH;
            _exit = true;
        }
        if (!_logs && COR != null)
        {
            var lH = new AllLogsHandler(stateManager, allLogsState);
            lH.SetNextHandler(COR);
            COR = lH;
            _logs = true;
        }
    }

    public void AddItems(int count)
    {
        if(! _pickup && COR != null)
        {
            var pH = new PickupHandler(player, map);
            pH.SetNextHandler(COR);
            COR = pH;
            _pickup = true;
        }
        if (!_drop && COR != null)
        {
            var dH = new DropHandler(player, map);
            dH.SetNextHandler(COR);
            COR = dH;
            _drop = true;
        }
    }

    public void AddWeapons(int count)
    {
        if (!_pickup && COR != null)
        {
            var pH = new PickupHandler(player, map);
            pH.SetNextHandler(COR);
            COR = pH;
            _pickup = true;
        }
        if(! _equip  && COR != null)
        {
            var eqH = new EquipHandler(player,map);
            eqH.SetNextHandler(COR);
            COR = eqH;
            _equip = true;
        }
        if(! _drop && COR != null)
        {
            var dH = new DropHandler(player, map);
            dH.SetNextHandler(COR);
            COR = dH;
            _drop = true;
        }
    }
    public void AddMobs(int count){}
    public void AddArtifact() {}
    public void AddMobGroup(Func<Mob> mobFactory, int count) { }
    public InputHandler? GetCOR()
    {
        return COR;
    }
}
