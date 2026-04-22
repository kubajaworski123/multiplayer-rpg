using MazeGame.Model.Loggers;
using System.Collections.Generic;

namespace MazeGame.Model.Observers
{
    public class LogBus : ILogPublisher
    {
        private readonly List<ILogObserver> _observers = new List<ILogObserver>();

        public void Subscribe(ILogObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Unsubscribe(ILogObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Publish(string message)
        {
            foreach (var observer in _observers)
            {
                observer.OnLog(message);
            }
        }
    }
}