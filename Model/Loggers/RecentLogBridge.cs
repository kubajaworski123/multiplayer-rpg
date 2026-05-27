using MazeGame.Model.Game;

namespace MazeGame.Model.Loggers;

public class RecentLogBridge(GameWorld world) : ILogObserver
{
    public void OnLog(string message) => world.AppendLog(message);
}
