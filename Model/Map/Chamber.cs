using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Map;

public class Chamber(int x, int y, int w, int h)
{
    public const int MaxW = 6;
    public const int MaxH = 3;
    public int X { get; private set; } = x;
    public int Y { get; private set; } = y;
    public int W { get; private set; } = w;
    public int H { get; private set; } = h;
    public int CX => X + W / 2;
    public int CY => Y + H / 2;
}
