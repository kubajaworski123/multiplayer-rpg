using MazeGame.Model.Items;
using MazeGame.Model.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.View;

public class FrameRenderer(Player player, Map map, List<string> controlsList)
{
    static readonly int middlePanelWidth = 41;
    static readonly int sidePanelWidth = (int)((Console.WindowWidth - middlePanelWidth) * 0.4);
    static readonly int leftPanelWidth = (int)((Console.WindowWidth - middlePanelWidth) * 0.25);
    public StringBuilder Frame { get; private set; } = new StringBuilder();
    public string? frameString;
    private List<string> ControlsList = controlsList;
    private Map map = map;
    private Player player = player;
    private const string banner = "|===[ＤＵＮＧＥＯＮＳ ＯＦ ＭＩＮＩ]===|";
    private Tile currTile => map.Tiles[player.X, player.Y];

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
        if (content == null) content = string.Empty;

        string prefix = "| ";
        string suffix = "| ";

        int minWidth = content.Length + prefix.Length + suffix.Length;
        if (width < minWidth)
        {
            width = minWidth;
        }

        int spaceNeeded = width - prefix.Length - suffix.Length;

        return prefix + content.PadRight(spaceNeeded) + suffix;
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
            GetSectionContentLine($" HP: {player.Health}",leftPanelWidth),
            GetSectionContentLine($"STR: {player.Strength}",leftPanelWidth),
            GetSectionContentLine($"AGI: {player.Agility}",leftPanelWidth),
            GetSectionContentLine($"WIS: {player.Wisdom}",leftPanelWidth),
            GetSectionContentLine($"HAP: {player.Happiness}",leftPanelWidth),
            GetSectionContentLine($"AGR: {player.Agressiveness}",leftPanelWidth),
            GetSectionContentLine($"INV: {player.InventoryCapacity}",leftPanelWidth),
            GetSectionContentLine($"GLD: {player.Gold}",leftPanelWidth),
            GetSectionContentLine($"MON: {player.Money}",leftPanelWidth),
            GetSectionBottomLine(leftPanelWidth)
        };
        return stats;
    }

    public List<string> FormatControlsList()
    {
        List<string> formatted = new List<string>();
        foreach(string control in ControlsList)
        {
            switch(control)
            {
                case ControlsListBuilder.equipControl:
                case ControlsListBuilder.dropControl:
                    if (player.Inventory.Count > 0)
                        formatted.Add(GetSectionContentLine(control, leftPanelWidth));
                break;
                case ControlsListBuilder.pickupControl:
                    if(currTile.MapEntities.Count > 0)
                        formatted.Add(GetSectionContentLine(control, leftPanelWidth));
                break;
                default:
                    formatted.Add(GetSectionContentLine(control, leftPanelWidth));
                break;
            }
        }
        formatted.Add(GetSectionBottomLine(leftPanelWidth));
        return formatted;
    }

    public List<string> GetInventoryList()
    {
        List<string> inventory = new List<string>();


        inventory.Add(GetTopSectionWithTitle("INVENTORY",sidePanelWidth));

        for (int i = 0; i < player.InventoryCapacity; i++)
        {
            string itemName = i < player.Inventory.Count ? player.Inventory[i].Name : "Empty";
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
            entities.Add(GetSectionContentLine("nothing here",sidePanelWidth));
        }
        else
        {
            foreach (var entity in currTile.MapEntities)
            {
                entities.Add(GetSectionContentLine($"{entity}",sidePanelWidth));
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
            StringBuilder mapRow = new StringBuilder();
            for (int x = 0; x < W; x++)
            {
                if (x == player.X && y == player.Y)
                    mapRow.Append("\x1b[31m"+'¶'+"\x1b[0m");
                else mapRow.Append(map.Tiles[x, y].Symbol);
            }
            mid.Add( mapRow.ToString().PadRight(middlePanelWidth + (y ==player.Y ? 9 : 0)));
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
    public void GetNewFrame()
    {
        Frame.Clear();
        Tile currTile = map.Tiles[player.X, player.Y];

        List<string> leftCol = GetLeftColumn();
        List<string> midCol = GetMiddleColumn();
        List<string> rightCol = GetRightColumn();

        int maxRows = Math.Max(leftCol.Count, Math.Max(midCol.Count, rightCol.Count));
        int totalWidth = sidePanelWidth + middlePanelWidth + leftPanelWidth;

        string emptySidePanelLine = new string(' ', sidePanelWidth);
        string emptyLeftPanelLine = new string(' ', leftPanelWidth);
        string emptyMidPanelLine = new string(' ', middlePanelWidth);

        Frame.AppendLine(emptyLeftPanelLine + banner + emptySidePanelLine);
        for (int i = 0; i < maxRows; i++)
        {
            string left = i < leftCol.Count ? leftCol[i] : emptyLeftPanelLine;
            string mid = i < midCol.Count ? midCol[i] : emptyMidPanelLine;
            string right = i < rightCol.Count ? rightCol[i] : emptySidePanelLine;

            Frame.Append(left);
            Frame.Append(mid);
            Frame.Append(right);
            Frame.AppendLine();
        }

        int linesToClear = Console.WindowHeight - maxRows - 2;
        if (linesToClear > 0)
        {
            string emptyLine = new string(' ', totalWidth);
            for (int i = 0; i < linesToClear; i++)
            {
                Frame.AppendLine(emptyLine);
            }
        }

        frameString = Frame.ToString();
    }

    public void RenderFrame()
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(frameString);
    }
}