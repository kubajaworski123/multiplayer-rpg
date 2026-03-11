using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.View;

public class Logger()
{
    private readonly List<string> logs = new List<string>(3);
    public int Count => logs.Count;

    public static Logger SingletonLogger { get; private set; } = new Logger();
    public string this[int idx]
    {
        get
        {
            if (idx >= 0 && idx < logs.Count)
            {
                return logs[idx];
            }
            else
            {
                return string.Empty;
            }
        }
    }
    public void Add(string log)
    {
        if (logs.Count < 3)
        {
            logs.Add(log);
        }
        else
        {
            logs[0] = logs[1];
            logs[1] = logs[2];
            logs[2] = log;
        }
    }

}
