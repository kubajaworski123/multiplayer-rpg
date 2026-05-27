using MazeGame.Controller.AllLogsHandlers;
using MazeGame.Controller.GameStates;
using MazeGame.Controller.UsersManualHandlers;
using MazeGame.View.Renderers;

namespace MazeGame.Controller;

public class NetworkOverlayManager
{
    public GameStateManager Inner { get; } = new();
    private readonly UserManualRenderer _manualRenderer;
    private readonly AllLogsRenderer _logsRenderer;
    private readonly List<string> _allLogs;
    private readonly UserManualState _manualState;
    private readonly AllLogsState _logsState;

    public NetworkOverlayManager(List<string> usersManual)
    {
        _allLogs = new List<string>();
        _manualRenderer = new UserManualRenderer(usersManual);
        _manualRenderer.Prepare();
        var exitUm = new ExitUsersManualHandler(Inner);
        _manualState = new UserManualState(_manualRenderer, exitUm);
        var scrollUp = new ScrollUpHandler(_manualRenderer);
        var scrollDown = new ScrollDownHandler(_manualRenderer);
        exitUm.SetNextHandler(scrollUp);
        scrollUp.SetNextHandler(scrollDown);

        _logsRenderer = new AllLogsRenderer(_allLogs);
        var exitLogs = new ExitAllLogsHandler(Inner);
        var scrollLogs = new ScrollAllLogsHandler(_logsRenderer);
        exitLogs.SetNextHandler(scrollLogs);
        _logsState = new AllLogsState(_logsRenderer, exitLogs);
    }

    public bool HasOverlay => Inner.HasActiveState;

    public void SyncLogs(IEnumerable<string> logs)
    {
        _allLogs.Clear();
        _allLogs.AddRange(logs);
    }

    public void PushManual()
    {
        _manualRenderer.ResetScroll();
        Inner.PushState(_manualState);
    }
    public void PushLogs()
    {
        _logsRenderer.Prepare();
        Inner.PushState(_logsState);
    }
    public void HandleInput(ConsoleKeyInfo key) => Inner.HandleInput(key);
    public void Render() => Inner.Render();
}
