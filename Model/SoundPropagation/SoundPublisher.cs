using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.SoundPropagation
{
    public class SoundPublisher
    {
        private readonly List<ISoundSubscriber> _subscribers = new List<ISoundSubscriber>();
        public static SoundPublisher Instance { get; private set; } = new SoundPublisher();

        public void Subscribe(ISoundSubscriber observer)
        {
            if (!_subscribers.Contains(observer))
                _subscribers.Add(observer);
        }

        public void Unsubscribe(ISoundSubscriber observer)
        {
            _subscribers.Remove(observer);
        }

        public void Publish(SoundData soundData)
        {
            foreach (var observer in _subscribers.ToArray())
            {
                observer.OnSound(soundData);
            }
        }
    }
}
