using MazeGame.Model.Loggers;
using System.Collections.Generic;

namespace MazeGame.Model.Observers
{
    public class AllLogsObserver : ILogObserver
    {
        public List<string> AllLogs { get; private set; } = new List<string>();

        public void OnLog(string message)
        {
            AllLogs.Add(message);
        }
    }
}