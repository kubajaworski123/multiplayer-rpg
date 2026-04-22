using MazeGame.Model.Loggers;
using System.Collections.Generic;

namespace MazeGame.Model.Observers
{
    public class MapViewLogger : ILogObserver
    {
        private readonly List<string> _logs = new List<string>(3);

        public string this[int index]
        {
            get
            {
                if (index >= 0 && index < _logs.Count)
                {
                    return _logs[index];
                }
                return string.Empty;
            }
        }

        public void OnLog(string message)
        {
            if (_logs.Count >= 3)
            {
                _logs.RemoveAt(0);
            }
            _logs.Add(message);
        }
    }
}