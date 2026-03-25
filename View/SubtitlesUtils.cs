using System;
using System.Threading;

public static class SubtitlesUtils
{
    public static void PlayImperialMarchLooped(CancellationToken token)
    // melody source: https://github.com/fleschutz/PowerShell/blob/main/scripts/play-imperial-march.ps1
    {
        int G4 = 392;
        int Eb4 = 311;
        int Bb4 = 466;
        int D5 = 587;
        int Eb5 = 622;
        int Gb4 = 370;

        int Q = 500;  
        int dE = 350; 
        int S = 150;  
        int H = 1000; 

        var melody = new (int Frequency, int Duration)[]
        {
            (G4, Q), (G4, Q), (G4, Q), (Eb4, dE), (Bb4, S),
            (G4, Q), (Eb4, dE), (Bb4, S), (G4, H),

            (D5, Q), (D5, Q), (D5, Q), (Eb5, dE), (Bb4, S),
            (Gb4, Q), (Eb4, dE), (Bb4, S), (G4, H)
        };

        while (!token.IsCancellationRequested)
        {
            foreach (var note in melody)
            {
                if(token.IsCancellationRequested)
                    return;
                if (OperatingSystem.IsWindows())
                {
                    Console.Beep(note.Frequency, note.Duration);
                }
            }
            Thread.Sleep(500);
        }
    }
    public static string GenerateStarLine(int width, string text, bool highlightText = false)
    {
        Random _random = new Random();
        char[] lineChars = new char[width];
        Array.Fill(lineChars, ' ');

        for (int i = 0; i < width; i++)
        {
            if (_random.NextDouble() < 0.08)
            {
                lineChars[i] = '.';
            }
        }

        if (string.IsNullOrEmpty(text))
        {
            return new string(lineChars);
        }

        int startIdx = (width - text.Length) / 2;
        if (startIdx < 0) startIdx = 0;

        for (int i = Math.Max(0, startIdx - 1); i < Math.Min(width, startIdx + text.Length + 1); i++)
        {
            lineChars[i] = ' ';
        }

        string fullLine = new string(lineChars);
        string leftPart = fullLine.Substring(0, startIdx);
        string rightPart = fullLine.Substring(startIdx + text.Length);

        string centerPart = highlightText ? $"\x1b[47;30m{text}\x1b[0m" : text;

        return leftPart + centerPart + rightPart;
    }
}