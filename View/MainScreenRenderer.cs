using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MazeGame.View;

public class MainScreenRenderer
{
    private StringBuilder MainScreen = new StringBuilder();
    string mainScreenString = string.Empty;

    private List<string> banner = new List<string>()
    {
        "\x1b[31m" +
        "▄▄▄      ▄▄▄    ▄▄▄    ▄▄▄ ▄▄▄▄▄   ▄▄▄▄▄▄   ▄▄▄  ▄▄▄ ▄▄▄    ▄▄▄  ▄▄▄▄▄▄▄   ▄▄▄▄▄▄▄   ▄▄▄▄▄   ▄▄▄    ▄▄▄ ",
        "████▄  ▄████ ▀▀ ████▄  ███  ███    ███▀▀██▄ ███  ███ ████▄  ███ ███▀▀▀▀▀  ███▀▀▀▀▀ ▄███████▄ ████▄  ███ ",
        "███▀████▀███ ██ ███▀██▄███  ███    ███  ███ ███  ███ ███▀██▄███ ███       ███▄▄    ███   ███ ███▀██▄███ ",
        "███  ▀▀  ███ ██ ███  ▀████  ███    ███  ███ ███▄▄███ ███  ▀████ ███  ███▀ ███      ███▄▄▄███ ███  ▀████ ",
        "███      ███ ██ ███    ███ ▄███▄   ██████▀  ▀██████▀ ███    ███ ▀██████▀  ▀███████  ▀█████▀  ███    ███ " +
        "\x1b[0m",
        "                                                                                                        ",
        "                                    ENTER ANY KEY TO CONTINUE                                           ",
        "                             FOR BETTER EXPERIENCE MAXIMIZE THE WINDOW                                  "
    };

    public void CalculateMainScreen()
    {
        Console.Clear();
        int bannerLength = banner[1].Length;
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;
        int emptyLinesBefore = (consoleHeight - banner.Count) / 2;
        int emptySpacesBefore = (consoleWidth - bannerLength) / 2;
        for (int i = 0; i < emptyLinesBefore; i++)
        {
            MainScreen.AppendLine();
        }
        foreach (var line in banner)
        {
            MainScreen.Append(' ', emptySpacesBefore);
            MainScreen.AppendLine(line);
        }
        for (int i = 0; i < emptyLinesBefore; i++)
        {
            MainScreen.AppendLine();
        }
        mainScreenString = MainScreen.ToString();
    }
    public void RenderMainScreen()
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(mainScreenString);
        Console.ReadKey();
        Console.Clear();
    }
}