using MazeGame.View;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class SWIntroRenderer(List <string> subs) : IRenderer
{
    private Random _random = new Random();
    private List<string> _subs = subs;

    public void Prepare()
    {
    }
    public void Render()
    {
        Console.Clear();
        int height = Console.WindowHeight;
        int drawWidth = Console.WindowWidth - 1;
        List<string> canvas = new List<string>();

        for (int i = 0; i < height; i++)
        {
            canvas.Add(ViewUtils.GenerateStarLine(drawWidth, string.Empty));
        }

        foreach (var line in _subs)
        {
            canvas.Add(ViewUtils.GenerateStarLine(drawWidth, line));
        }
        for (int i = 0; i < 2; i++)
        {
            canvas.Add(ViewUtils.GenerateStarLine(drawWidth, string.Empty));
        }

        int maxBannerWidth = 0;
        foreach (var b in TextBanners.INTRO )
        {
            if (b.Length > maxBannerWidth) maxBannerWidth = b.Length;
        }
        foreach (var line in TextBanners.INTRO )
        {
            string padded = line.PadRight(maxBannerWidth);
            canvas.Add(ViewUtils.GenerateStarLine(drawWidth, padded));
        }
        for (int i = 0; i < height; i++)
        {
            canvas.Add(ViewUtils.GenerateStarLine(drawWidth, string.Empty));
        }
        int totalFrames = canvas.Count - height;
        for (int frame = 0; frame <= totalFrames; frame++)
        {
            Console.SetCursorPosition(0, 0);
            StringBuilder screenBuffer = new StringBuilder();
            for (int lineIdx = 0; lineIdx < height; lineIdx++)
            {
                screenBuffer.Append(canvas[frame + lineIdx]);
                if (lineIdx < height - 1)
                {
                    screenBuffer.AppendLine();
                }
            }
            Console.Write("\x1b[93m" + screenBuffer.ToString() + "\x1b[0m");

            Thread.Sleep(1000);

            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                break;
            }
        }
        Console.Clear();
    }
   
}