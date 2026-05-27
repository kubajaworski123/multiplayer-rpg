using MazeGame.Controller.GameStates;
using MazeGame.Controller.MainMapHandlers;
using MazeGame.Model.Builders;
using MazeGame.Model.Characters;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Players;
using MazeGame.View.Renderers;

namespace MazeGame.Controller.Builders;

public class MapInputChainBuilder(Player player, Map map, FrameRenderer renderer,
    GameStateManager stateManager, UserManualState manualState, GameOverState gameOverState, AllLogsState allLogsState) : IMapBuilder
{
    private bool _move = false;
    private bool _exit = false;
    private bool _pickup = false;
    private bool _equip = false;
    private bool _drop = false;
    private bool _logs = false;

    public InputHandler? COR { get; private set; } = new UnknownInputHandler(map, player);

    public void BuildCentralHall(int width, int height) => AddMove();
    public void BuildChambers(int N) => AddMove();
    public void BuildCorridors(int turns) => AddMove();

    public void BuildEmptyDungeon(int width, int height)
    {
        var umh = new UsersManualHandler(stateManager, manualState);
        umh.SetNextHandler(COR);
        COR = umh;
        AddMove();
        AddExit();
        AddLogs();
    }

    public void BuildFilledDungeon(int width, int height)
    {
        var umh = new UsersManualHandler(stateManager, manualState);
        umh.SetNextHandler(COR);
        COR = umh;
        AddExit();
        AddLogs();
    }

    public void AddItems(int count)
    {
        AddPickup();
        AddDrop();
    }

    public void AddWeapons(int count)
    {
        AddPickup();
        AddEquip();
        AddDrop();
    }

    public void AddMobs(int count) { }
    public void AddArtifact() { }
    public void AddMobGroup(Func<Mob> mobFactory, int count) { }

    public InputHandler? GetCOR() => COR;

    private void AddMove()
    {
        if (_move || COR == null) return;
        var mh = new MovementHandler(player, map, stateManager);
        mh.SetNextHandler(COR);
        COR = mh;
        _move = true;
    }

    private void AddExit()
    {
        if (_exit || COR == null) return;
        var eh = new ExitHandler(stateManager, gameOverState);
        eh.SetNextHandler(COR);
        COR = eh;
        _exit = true;
    }

    private void AddLogs()
    {
        if (_logs || COR == null) return;
        var lh = new AllLogsHandler(stateManager, allLogsState);
        lh.SetNextHandler(COR);
        COR = lh;
        _logs = true;
    }

    private void AddPickup()
    {
        if (_pickup || COR == null) return;
        var ph = new PickupHandler(player, map);
        ph.SetNextHandler(COR);
        COR = ph;
        _pickup = true;
    }

    private void AddDrop()
    {
        if (_drop || COR == null) return;
        var dh = new DropHandler(player, map);
        dh.SetNextHandler(COR);
        COR = dh;
        _drop = true;
    }

    private void AddEquip()
    {
        if (_equip || COR == null) return;
        var eq = new EquipHandler(player, map);
        eq.SetNextHandler(COR);
        COR = eq;
        _equip = true;
    }
}
