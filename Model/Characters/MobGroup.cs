using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Characters
{
    public class MobGroup : IMobPublisher
    {
        private List<IMobSubscriber> _members = new List<IMobSubscriber>();

        public void Subscribe(IMobSubscriber observer)
        {
            if (!_members.Contains(observer))
            {
                _members.Add(observer);
            }
        }

        public void Unsubscribe(IMobSubscriber observer)
        {
            _members.Remove(observer);
        }

        public void NotifyDeath(IMobSubscriber deceased)
        {
            Unsubscribe(deceased);

            foreach (var member in _members.ToList())
            {
                member.OnFriendsDeath();
            }
        }
    }
}
