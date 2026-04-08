using MazeGame.Model.Builders;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Players;
using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MazeGame.View.Renderers;
public class FrameRenderer(Player player, Map map, List<string> controlsList) : IRenderer
{

    static readonly int middlePanelWidth = 41;
    static readonly int sidesStarsWidth = 0;
    static readonly int sidePanelWidth = (int)((Console.WindowWidth - middlePanelWidth - sidesStarsWidth) * 0.7);
    static readonly int leftPanelWidth = (int)((Console.WindowWidth - middlePanelWidth - sidesStarsWidth) * 0.23);

    public StringBuilder Frame { get; private set; } = new StringBuilder();
    public string? frameString;
    private List<string> ControlsList = controlsList;
    private Map map = map;
    private Player player = player;
    public int? SelectedEntityIndex { get; set; } = null;
    private Tile currTile => map.Tiles[player.X, player.Y];

    private List<string> _banner = new List<string>()
    {   
        string.Empty,
         " __  __   ___   _  _   ___     ___    _   _   _  _   ___   ___  ___   _  _   ___    .",
        @"|  \/  | |_ _| | \| | |_ _|   |   \  | | | | | \| | / __| | __ / _ \ | \| | / __| .  ",
        @"| |\/| |  | |  | .` |  | |    | |) | | |_| | | .` | |(_ | | _| |(_)| | .` | \__ \.   ",
        @"|_|  |_| |___| |_|\_| |___|   |___/   \___/  |_|\_| \___| |___ \___/ |_|\_| |___/    ",
        string.Empty
    };

    public static string GetTopSectionWithTitle(string title, int width)
    {
        if (title == null) title = string.Empty;

        int minWidth = title.Length + 2;
        if (width < minWidth)
        {
            width = minWidth;
        }

        int totalDashes = width - 3 - title.Length;
        int leftDashes = totalDashes / 2;
        int rightDashes = totalDashes - leftDashes;

        return "|" + new string('-', leftDashes) + title + new string('-', rightDashes) + "| ";
    }
    public static string GetSectionContentLine(string content, int width)
    {
        content ??= string.Empty;
        string prefix = "| ";
        string suffix = "| ";

        int visibleContentLen = content.GetVisibleLength();
        int minWidth = visibleContentLen + prefix.Length + suffix.Length;

        if (width < minWidth) width = minWidth;

        int spaceNeeded = width - prefix.Length - suffix.Length;
        return prefix + content.VisiblePadRight(spaceNeeded) + suffix;
    }
    public static string GetSectionBottomLine(int width)
    {
        return '|' + new string('-',width-3) + "| ";
    }
    public List<string> GetStatsList()
    {
        List<string> stats = new List<string>()
        {
            GetTopSectionWithTitle("STATS", leftPanelWidth),
            GetSectionContentLine($" HP: {player.Stats.Health}",leftPanelWidth),
            GetSectionContentLine($"STR: {player.Stats.Strength}",leftPanelWidth),
            GetSectionContentLine($"AGI: {player.Stats.Agility}",leftPanelWidth),
            GetSectionContentLine($"WIS: {player.Stats.Wisdom}",leftPanelWidth),
            GetSectionContentLine($"HAP: {player.Stats.Happiness}",leftPanelWidth),
            GetSectionContentLine($"AGR: {player.Stats.Agressiveness}",leftPanelWidth),
            GetSectionContentLine($"LCK: {player.Stats.Luck}",leftPanelWidth),
            GetSectionContentLine($"INV: {player.Backpack.InventoryCapacity}",leftPanelWidth),
            GetSectionContentLine($"GLD: {player.Backpack.Gold}",leftPanelWidth),
            GetSectionContentLine($"MON: {player.Backpack.Money}",leftPanelWidth),
            GetSectionBottomLine(leftPanelWidth)
        };
        return stats;
    }

    public List<string> FormatControlsList()
    {
        List<string> formatted = new List<string>();
        foreach (string control in ControlsList)
        {
            var line = control switch
            {
                var c when c == ControlsListBuilder.equipControl || c == ControlsListBuilder.dropControl =>
                    player.Backpack.Inventory.Count > 0 ? GetSectionContentLine(control, leftPanelWidth) : null,

                var c when c == ControlsListBuilder.pickupControl =>
                    currTile.MapEntities.Count > 0 ? GetSectionContentLine(control, leftPanelWidth) : null,

                _ => GetSectionContentLine(control, leftPanelWidth)
            };

            if (line != null) formatted.Add(line);
        }
        formatted.Add(GetSectionBottomLine(leftPanelWidth));
        return formatted;
    }

    public List<string> GetInventoryList()
    {
        List<string> inventory = new List<string>();
        inventory.Add(GetTopSectionWithTitle("INVENTORY",sidePanelWidth));

        for (int i = 0; i < player.Backpack.InventoryCapacity; i++)
        {
            string itemName = i < player.Backpack.Inventory.Count ? player.Backpack.Inventory[i].ToString() : "Empty";
            inventory.Add(GetSectionContentLine($"{i}: {itemName}", sidePanelWidth));
        }
        return inventory;
    }
    public List<string> GetHandsList()
    {
        StringBuilder lH = new StringBuilder();
        StringBuilder rH = new StringBuilder();

        if (player.LeftHand != null)
        {
            lH.Append(player.LeftHand.Name).Append(' ').Append(player.LeftHand.Damage);
        }
        else
        {
            lH.Append("Empty");
        }

        if (player.RightHand != null)
        {
            rH.Append(player.RightHand.Name).Append(' ').Append(player.RightHand.Damage);
        }
        else
        {
            rH.Append("Empty");
        }

        List<string> hands = new List<string>()
        {
            GetTopSectionWithTitle("HANDS",sidePanelWidth),
            GetSectionContentLine($"L Hand: {lH}",sidePanelWidth),
            GetSectionContentLine($"R Hand: {rH}",sidePanelWidth),
        };

        return hands;
    }
    public List<string> GetEntitiesFromTile()
    {
        List<string> entities = new List<string>()
    {
        GetTopSectionWithTitle("ITEMS HERE", sidePanelWidth)
    };

        if (currTile.MapEntities.Count == 0)
        {
            entities.Add(GetSectionContentLine("nothing here", sidePanelWidth));
        }
        else
        {
            for (int i = 0; i < currTile.MapEntities.Count; i++)
            {
                string entityString = currTile.MapEntities[i].ToString();
                if (SelectedEntityIndex.HasValue && SelectedEntityIndex.Value == i)
                {
                    entities.Add(GetSectionContentLine($"\x1b[47;30m{entityString}\x1b[0m", sidePanelWidth));
                }
                else
                {
                    entities.Add(GetSectionContentLine($"{entityString}", sidePanelWidth));
                }
            }
        }
        entities.Add(GetSectionBottomLine(sidePanelWidth));
        return entities;
    }
    public List <string> GetLoggerList()
    {
        List <string> log = new List<string>()
        {
            GetTopSectionWithTitle("LOGGER",middlePanelWidth)
        };
        for (int i = 0; i < 3; i++)
        {
            log.Add(GetSectionContentLine($"{Logger.SingletonLogger[i]}", middlePanelWidth));
        }
        log.Add(GetSectionBottomLine(middlePanelWidth));
        return log;
    }
    private List<string> GetLeftColumn()
    {
        List<string> left = GetStatsList();
        left.AddRange(FormatControlsList());
        return left;
    }
    private List<string> GetMiddleColumn()
    {
        List<string> mid = new List<string>();
        int W = map.Width;

        for (int y = 0; y < map.Height; y++)
        {
            StringBuilder mapRow = new StringBuilder("\x1b[0m");
            for (int x = 0; x < W; x++)
            {
                var combatantsHere = map.GetCombatantsFromTile(x, y);
                if (x == player.X && y == player.Y)
                    mapRow.Append('¶');
                else if (combatantsHere.Count > 0)
                    mapRow.Append(combatantsHere.First().ColorCode + combatantsHere.First().Symbol + ViewUtils.ResetCode);
                else mapRow.Append(map[x, y].Symbol);
            }
            mapRow.Append(" \x1b[93m");
            mid.Add( mapRow.ToString().VisiblePadRight(middlePanelWidth ));
        }
        mid.AddRange(GetLoggerList());
        return mid;
    }
    private List<string> GetRightColumn()
    {
        List<string> right = new List<string>();
        right.AddRange(GetHandsList());
        right.AddRange(GetInventoryList());
        right.AddRange(GetEntitiesFromTile());
        return right;
    }
    public void Prepare()
    {
        Frame.Clear();
        Tile currTile = map.Tiles[player.X, player.Y];
        bool hasBanner = false;

        List<string> leftCol = GetLeftColumn();
        List<string> midCol = GetMiddleColumn();
        List<string> rightCol = GetRightColumn();

        int maxRows = Math.Max(leftCol.Count, Math.Max(midCol.Count, rightCol.Count));
        int totalWidth = sidePanelWidth + middlePanelWidth + leftPanelWidth + sidesStarsWidth;

        string emptySidePanelLine = ViewUtils.GenerateStarLine(sidePanelWidth, string.Empty, false, 1);
        string emptyLeftPanelLine = ViewUtils.GenerateStarLine(leftPanelWidth, string.Empty, false,  1);
        string emptyMidPanelLine = ViewUtils.GenerateStarLine(middlePanelWidth, string.Empty, false,  1);

        if (maxRows + _banner.Count < Console.WindowHeight)
        {
            hasBanner = true;
            for (int i = 0; i < _banner.Count; i++)
            {
                string bannerLine = _banner[i];
                Frame.Append(ViewUtils.GenerateStarLine(Console.WindowWidth, bannerLine, false, i + 2137));
                Frame.AppendLine();
            }
        }

        for (int i = 0; i < maxRows; i++)
        {
            emptySidePanelLine = ViewUtils.GenerateStarLine(sidePanelWidth, string.Empty, false, i + 1);
            emptyLeftPanelLine = ViewUtils.GenerateStarLine(leftPanelWidth, string.Empty, false, i + 1);
            emptyMidPanelLine = ViewUtils.GenerateStarLine(middlePanelWidth, string.Empty, false, i + 1);

            string left = i < leftCol.Count ? leftCol[i] : emptyLeftPanelLine;
            string mid = i < midCol.Count ? midCol[i] : emptyMidPanelLine;
            string right = i < rightCol.Count ? rightCol[i] : emptySidePanelLine;

            string line = left + mid + right;
            Frame.Append(ViewUtils.GenerateStarLine(Console.WindowWidth, line, false, i+1));
            Frame.AppendLine();
        }

        int linesToClear = Console.WindowHeight - maxRows - (hasBanner? _banner.Count : 0) -1;
        if (linesToClear > 0)
        {
            for (int i = 0; i < linesToClear; i++)
            {
                string emptyLine = ViewUtils.GenerateStarLine(Console.WindowWidth, string.Empty, false, i+1);
                Frame.AppendLine(emptyLine);
            }
        }

        frameString = Frame.ToString();
    }

    public void Render()
    {
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(frameString);
        Console.ForegroundColor= ConsoleColor.White;
    }
}