using MazeGame.Model.Characters;
using MazeGame.Networking.Dto;
using System.Text;

namespace MazeGame.View.Renderers;

public class NetworkCombatRenderer
{
    public void Render(CombatDto combat)
    {
        int portraitPanelWidth = (int)(Console.WindowWidth * 0.35);
        int middlePanelWidth = Console.WindowWidth - 2 * portraitPanelWidth;

        Enum.TryParse<CharacterType>(combat.LeftType, out var leftType);
        Enum.TryParse<CharacterType>(combat.RightType, out var rightType);
        var leftBanner = ViewUtils.TypeToBanner(leftType);
        var rightBanner = ViewUtils.TypeToBanner(rightType);

        var leftPanel = BuildPanel(combat.LeftName, combat.LeftHealth, leftBanner);
        var rightPanel = BuildPanel(combat.RightName, combat.RightHealth, rightBanner);
        var middlePanel = BuildMiddle(combat);

        int longer = Math.Max(leftPanel.Count, Math.Max(rightPanel.Count, middlePanel.Count));
        var screenLines = new List<string>(longer);
        for (int y = 0; y < longer; y++)
        {
            string left = y < leftPanel.Count
                ? ViewUtils.GenerateStarLine(portraitPanelWidth, ViewUtils.ResetCode + leftPanel[y] + ViewUtils.YellowCode)
                : ViewUtils.GenerateStarLine(portraitPanelWidth, string.Empty, false, y);
            string right = y < rightPanel.Count
                ? ViewUtils.GenerateStarLine(portraitPanelWidth, ViewUtils.ResetCode + rightPanel[y] + ViewUtils.YellowCode)
                : ViewUtils.GenerateStarLine(portraitPanelWidth, string.Empty, false, y);
            bool highlight = combat.ActiveCombat && y == 2 + TextBanners.VS.Count + combat.HighlightedAttack;
            string mid = y < middlePanel.Count
                ? ViewUtils.GenerateStarLine(middlePanelWidth, middlePanel[y], highlight)
                : ViewUtils.GenerateStarLine(middlePanelWidth, string.Empty, false, y);
            screenLines.Add(left.VisiblePadRight(portraitPanelWidth) + mid.VisiblePadRight(middlePanelWidth) + right.VisiblePadRight(portraitPanelWidth));
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        ViewUtils.WriteFullScreen(screenLines);
        Console.ResetColor();
    }

    private static List<string> BuildPanel(string name, int health, List<string> banner)
    {
        var panel = new List<string> { string.Empty, name.ToUpper() };
        panel.AddRange(banner);
        int w = banner[0].Length;
        int filled = health * (w - 3) / 100;
        panel.Add("HP:" + ViewUtils.RedCode + new string('█', filled) + new string(' ', w - 3 - filled) + ViewUtils.ResetCode);
        return panel;
    }

    private static List<string> BuildMiddle(CombatDto combat)
    {
        var mid = new List<string> { string.Empty };
        if (combat.Outcome == "RightWon")
        {
            mid.AddRange(TextBanners.YOU);
            mid.Add(string.Empty);
            mid.AddRange(TextBanners.LOSE);
            mid.Add("PRESS ANY KEY");
        }
        else if (combat.Outcome == "LeftWon")
        {
            mid.AddRange(TextBanners.YOU);
            mid.Add(string.Empty);
            mid.AddRange(TextBanners.WIN);
            mid.Add("PRESS ANY KEY");
        }
        else
        {
            mid.AddRange(TextBanners.VS);
            mid.Add(string.Empty);
            mid.AddRange(combat.AttackNames);
        }
        return mid;
    }
}
