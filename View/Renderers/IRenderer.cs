using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.View.Renderers;

public interface IRenderer
{
    void Prepare();
    void Render();
}
