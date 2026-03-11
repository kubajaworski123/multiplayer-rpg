using MazeGame.Model.Items;
using MazeGame.Model.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.View;

public class FrameRenderer
{
    const int sidePanelTextWidth = -26;
    const int sidePanelWidth = 31;
    const int middlePanelWidth = 40;

    public StringBuilder Frame { get; private set; } = new StringBuilder();
    public string? frameString;

    public static List<string> GetStatsList(Player player)
    {
        List<string> stats = new List<string>()
        {
             " |-----------STATS-----------| ",
            $" | {$"❤️: {player.Health}", sidePanelTextWidth}| ",
            $" | {$"⚔️: {player.Strength}", sidePanelTextWidth}| ",
            $" | {$"🏃: {player.Agility}", sidePanelTextWidth}| ",
            $" | {$"🧠: {player.Wisdom}", sidePanelTextWidth}| ",
            $" | {$"🙂: {player.Happiness}", sidePanelTextWidth}| ",
            $" | {$"😡: {player.Agressiveness}", sidePanelTextWidth}| ",
            $" | {$"🎒: {player.InventoryCapacity}", sidePanelTextWidth}| ",
            $" | {$"🪙: {player.Gold}", sidePanelTextWidth}| ",
            $" | {$"💰: {player.Money}", sidePanelTextWidth}| ",
             " |---------------------------| "
        };
        return stats;
    }
    public static List<string> GetInventoryList(Player player)
    {
        List<string> inventory = new List<string>()
        {
            " |---------INVENTORY---------| "
        };

        for (int i = 0; i < player.InventoryCapacity; i++)
        {
            string itemName = i < player.Inventory.Count ? player.Inventory[i].Name : "Empty";
            inventory.Add($" | {$"{i}: {itemName}",sidePanelTextWidth}| ");
        }
        return inventory;
    }
    public static List<string> GetHandsList(Player player)
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
             " |-----------HANDS-----------| ",
            $" | {$"L Hand: {lH}", sidePanelTextWidth}| ",
            $" | {$"R Hand: {rH}", sidePanelTextWidth}| "
        };
        return hands;
    }
    public static List<string> GetEntitiesFromTile(Tile tile)
    {
        List<string> entities = new List<string>()
        {
            " |--------ITEMS HERE---------| "
        };

        if (tile.MapEntities.Count == 0)
        {
            entities.Add($" | {"Nothing on this tile",sidePanelTextWidth}| ");
        }
        else
        {
            foreach (var entity in tile.MapEntities)
            {
                entities.Add($" | {$"{entity}",sidePanelTextWidth}| ");
            }
        }
        entities.Add(" |---------------------------| ");
        return entities;
    }
    public static List<string> GetControlsList()
    {
        List<string> controls = new List<string>()
        {
            "|--------------CONTROLS----------------|",
            $"| {"ESC: Exit | WSAD: Move | E: Pick Up",-(middlePanelWidth-3)}|",
            $"| {"0-9: Equip | alt+0-9: Drop",-(middlePanelWidth-3)}|",
            "|--------------------------------------|"

        };
        return controls;
    }
    public static List <string> GetLoggerList()
    {
        List <string> log = new List<string>()
        {
            "|----------------LOGGER----------------|"
        };
        for (int i = 0; i < 3; i++)
        {
            log.Add($"| {$"{Logger.SingletonLogger[i]}",-(middlePanelWidth-3)}|");
        }
        log.Add("|--------------------------------------|");
        return log;
    }
    private static List<string> GetLeftColumn(Player player)
    {
        return GetStatsList(player);
    }
    private static List<string> GetMiddleColumn(Map map, Player player)
    {
        List<string> mid = new List<string>();
        int W = map.Width;

        mid.AddRange(GetControlsList());

        for (int y = 0; y < map.Height; y++)
        {
            StringBuilder mapRow = new StringBuilder();
            for (int x = 0; x < W; x++)
            {
                if (x == player.X && y == player.Y) mapRow.Append('¶');
                else mapRow.Append(map.Tiles[x, y].Symbol);
            }
            mid.Add(mapRow.ToString().PadRight(middlePanelWidth));
        }
        mid.AddRange(GetLoggerList());
        return mid;
    }
    private static List<string> GetRightColumn(Player player, Tile currTile)
    {
        List<string> right = new List<string>();
        right.AddRange(GetHandsList(player));
        right.AddRange(GetInventoryList(player));
        right.AddRange(GetEntitiesFromTile(currTile));
        return right;
    }
    public void GetNewFrame(Map map, Player player)
    {
        Frame.Clear();
        Tile currTile = map.Tiles[player.X, player.Y];

        List<string> leftCol = GetLeftColumn(player);
        List<string> midCol = GetMiddleColumn(map, player);
        List<string> rightCol = GetRightColumn(player, currTile);

        int maxRows = Math.Max(leftCol.Count, Math.Max(midCol.Count, rightCol.Count));
        int totalWidth = sidePanelWidth + middlePanelWidth + sidePanelWidth;

        string emptySidePanelLine = new string(' ', sidePanelWidth);
        string emptyMid = new string(' ', middlePanelWidth);

        for (int i = 0; i < maxRows; i++)
        {
            string left = i < leftCol.Count ? leftCol[i] : emptySidePanelLine;
            string mid = i < midCol.Count ? midCol[i] : emptyMid;
            string right = i < rightCol.Count ? rightCol[i] : emptySidePanelLine;

            Frame.Append(left);
            Frame.Append(mid);
            Frame.Append(right);
            Frame.AppendLine();
        }

        int linesToClear = Console.WindowHeight - maxRows - 1;
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
        Console.SetCursorPosition(Console.WindowLeft, Console.WindowTop);
        Console.Write(frameString);
    }
}