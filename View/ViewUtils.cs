using MazeGame.Model.Characters;
using MazeGame.Model.Items;
using MazeGame.View;
using System;
using System.Threading;

public static class ViewUtils
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
    public static string YellowCode { get; } = "\x1b[93m";
    public static string ResetCode { get; } = "\x1b[0m";
    public static string RedCode { get; } = "\x1b[31m";
    public static string BlueCode { get; } = "\x1b[34m";
    public static string GreenCode { get; } = "\x1b[32m";
    public static string PurpleCode { get; } = "\x1b[35m";
    public static string RarityToColor(ItemRarity rarity)
    {
        return rarity switch
        {
            ItemRarity.Common => YellowCode,
            ItemRarity.Fine => BlueCode,
            ItemRarity.Legendary => RedCode,
            ItemRarity.Artifact => GreenCode,
            _ => YellowCode
        };
    }
    public static List<string> TypeToBanner(CharacterType bannerType)
    {
        return bannerType switch
        {
            CharacterType.Machine => CharacterBanners.ATSTBanner,
            CharacterType.Droid => CharacterBanners.DroidBanner,
            CharacterType.Player => CharacterBanners.PlayerBanner,
            CharacterType.BountyHunter => CharacterBanners.BountyHunterBanner,
            CharacterType.Dead => CharacterBanners.DeadBanner,
            _ => CharacterBanners.PlayerBanner
        };
    }
    public static string TypeToColor(CharacterType type)
    {
        return type switch
        {
            CharacterType.Machine => BlueCode,
            CharacterType.Droid => PurpleCode,
            CharacterType.Player => YellowCode,
            CharacterType.BountyHunter => GreenCode,
            _ => ResetCode
        };
    }

    private static int GetVisibleLength(string text)
    {
        if (string.IsNullOrEmpty(text)) return 0;
        int length = 0;
        bool inEscape = false;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '\x1b') inEscape = true;
            else if (inEscape && text[i] == 'm') inEscape = false;
            else if (!inEscape) length++;
        }
        return length;
    }

    public static string GenerateStarLine(int width, string text, bool highlightText = false, int? seed = null)
    {
        Random _random = seed.HasValue ? new Random(seed.Value) : new Random();
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

        int visibleLength = GetVisibleLength(text);
        int startIdx = (width - visibleLength) / 2;

        if (visibleLength >= width)
        {
            return highlightText ? $"\x1b[47;30m{text}\x1b[0m" : text;
        }

        if (startIdx < 0) startIdx = 0;

        for (int i = Math.Max(0, startIdx - 1); i < Math.Min(width, startIdx + visibleLength + 1); i++)
        {
            lineChars[i] = ' ';
        }

        string fullLine = new string(lineChars);
        string leftPart = fullLine.Substring(0, startIdx);
        string rightPart = fullLine.Substring(Math.Min(width, startIdx + visibleLength));

        string centerPart = highlightText ? $"\x1b[47;30m{text}\x1b[0m" : text;

        return leftPart + centerPart + rightPart;
    }

}