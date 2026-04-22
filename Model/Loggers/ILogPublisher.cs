using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Loggers
{
    public interface ILogPublisher
    {
        void Subscribe(ILogObserver observer);
        void Unsubscribe(ILogObserver observer);
        void Publish(string message);
    }
}
