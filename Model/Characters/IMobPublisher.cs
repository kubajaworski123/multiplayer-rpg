using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Characters
{
    public interface IMobPublisher
    {
        void Subscribe(IMobSubscriber observer);
        void Unsubscribe(IMobSubscriber observer);
        void NotifyDeath(IMobSubscriber deceased);
    }
}
