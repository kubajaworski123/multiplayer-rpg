using System;

namespace MazeGame.View;

public static class StringExtensions
{
    public static int GetVisibleLength(this string text)
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

    public static string VisiblePadRight(this string text, int totalWidth)
    {
        int visibleLength = text.GetVisibleLength();
        int paddingNeeded = totalWidth - visibleLength;

        if (paddingNeeded <= 0) return text;

        return text + new string(' ', paddingNeeded);
    }
}