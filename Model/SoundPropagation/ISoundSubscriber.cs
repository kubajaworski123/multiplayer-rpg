using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.SoundPropagation
{
    public interface ISoundSubscriber
    {
        void OnSound(SoundData soundData);
    }
}
