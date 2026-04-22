using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Loggers
{
    public interface ILogObserver
    {
        void OnLog(string message);
    }
}
