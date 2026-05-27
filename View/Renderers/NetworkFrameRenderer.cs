using MazeGame.Model.Characters;
using MazeGame.Model.Items;
using MazeGame.Networking.Dto;
using System.Text;

namespace MazeGame.View.Renderers;

public class NetworkFrameRenderer
{
    static readonly int middlePanelWidth = 41;
    static readonly int sidesStarsWidth = 0;
    static int sidePanelWidth => (int)((Console.WindowWidth - middlePanelWidth - sidesStarsWidth) * 0.7);
    static int leftPanelWidth => (int)((Console.WindowWidth - middlePanelWidth - sidesStarsWidth) * 0.23);

    public void Render(GameStateDto state, int localPlayerId)
    {
        var player = state.Players.FirstOrDefault(p => p.Id == localPlayerId);
        if (player == null) return;

        var leftCol = BuildLeftColumn(state, player);
        var midCol = BuildMiddleColumn(state, player);
        var rightCol = BuildRightColumn(state, player);
        int maxRows = Math.Max(leftCol.Count, Math.Max(midCol.Count, rightCol.Count));

        string emptySide = ViewUtils.GenerateStarLine(sidePanelWidth, string.Empty, false, 1);
        string emptyLeft = ViewUtils.GenerateStarLine(leftPanelWidth, string.Empty, false, 1);
        string emptyMid = ViewUtils.GenerateStarLine(middlePanelWidth, string.Empty, false, 1);

        var screenLines = new List<string>(maxRows);
        for (int i = 0; i < maxRows; i++)
        {
            string left = i < leftCol.Count ? leftCol[i] : emptyLeft;
            string mid = i < midCol.Count ? midCol[i] : emptyMid;
            string right = i < rightCol.Count ? rightCol[i] : emptySide;
            screenLines.Add(ViewUtils.GenerateStarLine(Console.WindowWidth, left + mid + right, false, i + 1));
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        ViewUtils.WriteFullScreen(screenLines);
        Console.ForegroundColor = ConsoleColor.White;
    }

    private List<string> BuildLeftColumn(GameStateDto state, PlayerDto player)
    {
        var lines = new List<string>
        {
            FrameRenderer.GetTopSectionWithTitle("STATS", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($" HP: {player.Health}", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($"STR: {player.Stats.Strength}", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($"AGI: {player.Stats.Agility}", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($"WIS: {player.Stats.Wisdom}", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($"HAP: {player.Stats.Happiness}", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($"AGR: {player.Stats.Agressiveness}", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($"LCK: {player.Stats.Luck}", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($"INV: {player.Backpack.InventoryCapacity}", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($"GLD: {player.Backpack.Gold}", leftPanelWidth),
            FrameRenderer.GetSectionContentLine($"MON: {player.Backpack.Money}", leftPanelWidth),
            FrameRenderer.GetSectionBottomLine(leftPanelWidth)
        };
        foreach (var c in state.Controls)
            lines.Add(FrameRenderer.GetSectionContentLine(c, leftPanelWidth));
        lines.Add(FrameRenderer.GetSectionBottomLine(leftPanelWidth));
        return lines;
    }

    private List<string> BuildMiddleColumn(GameStateDto state, PlayerDto player)
    {
        var mid = new List<string>();
        int W = state.Map.Width;
        int H = state.Map.Height;
        var tileLookup = state.Map.Tiles.ToDictionary(t => (t.X, t.Y));
        var playersByPos = state.Players.Where(p => p.IsConnected).ToLookup(p => (p.X, p.Y));
        var mobsByPos = state.Map.Combatants.Where(c => !c.IsPlayer).ToLookup(c => (c.X, c.Y));

        for (int y = 0; y < H; y++)
        {
            var row = new StringBuilder(ViewUtils.ResetCode);
            for (int x = 0; x < W; x++)
            {
                if (playersByPos.Contains((x, y)))
                {
                    var pl = playersByPos[(x, y)].First();
                    char ch = (char)('0' + pl.Id);
                    if (pl.Id == player.Id)
                        row.Append('¶');
                    else
                        row.Append(ViewUtils.BlueCode).Append(ch).Append(ViewUtils.ResetCode);
                }
                else if (mobsByPos.Contains((x, y)))
                {
                    var mob = mobsByPos[(x, y)].First();
                    if (Enum.TryParse<CharacterType>(mob.Type, out var ct))
                        row.Append(ViewUtils.TypeToColor(ct) + mob.Symbol + ViewUtils.ResetCode);
                    else
                        row.Append(mob.Symbol);
                }
                else if (tileLookup.TryGetValue((x, y), out var tile))
                    row.Append(tile.Symbol);
                else
                    row.Append(' ');
            }
            row.Append(" \x1b[93m");
            mid.Add(row.ToString().VisiblePadRight(middlePanelWidth));
        }
        mid.AddRange(BuildLogs(state.RecentLogs));
        return mid;
    }

    private List<string> BuildLogs(List<string> logs)
    {
        var log = new List<string> { FrameRenderer.GetTopSectionWithTitle("LOGGER", middlePanelWidth) };
        int maxW = middlePanelWidth - 4;
        var recent = logs.TakeLast(3).ToList();
        while (recent.Count < 3) recent.Insert(0, "");
        foreach (var line in recent)
            log.Add(FrameRenderer.GetSectionContentLine(line.Length > maxW ? line[..maxW] : line, middlePanelWidth));
        log.Add(FrameRenderer.GetSectionBottomLine(middlePanelWidth));
        return log;
    }

    private List<string> BuildRightColumn(GameStateDto state, PlayerDto player)
    {
        var tile = state.Map.Tiles.FirstOrDefault(t => t.X == player.X && t.Y == player.Y);
        var right = new List<string>();
        right.Add(FrameRenderer.GetTopSectionWithTitle("HANDS", sidePanelWidth));
        right.Add(FrameRenderer.GetSectionContentLine($"L Hand: {FormatItem(player.LeftHand)}", sidePanelWidth));
        right.Add(FrameRenderer.GetSectionContentLine($"R Hand: {FormatItem(player.RightHand)}", sidePanelWidth));
        right.Add(FrameRenderer.GetTopSectionWithTitle("INVENTORY", sidePanelWidth));
        for (int i = 0; i < player.Backpack.InventoryCapacity; i++)
        {
            string item = i < player.Backpack.Inventory.Count ? FormatItem(player.Backpack.Inventory[i]) : "";
            right.Add(FrameRenderer.GetSectionContentLine($"{i}: {item}", sidePanelWidth));
        }
        right.Add(FrameRenderer.GetTopSectionWithTitle("ITEMS HERE", sidePanelWidth));
        if (tile == null || tile.Entities.Count == 0)
            right.Add(FrameRenderer.GetSectionContentLine("nothing here", sidePanelWidth));
        else
            foreach (var e in tile.Entities)
                right.Add(FrameRenderer.GetSectionContentLine(FormatItem(e), sidePanelWidth));
        right.Add(FrameRenderer.GetSectionBottomLine(sidePanelWidth));
        return right;
    }

    private static string FormatItem(ItemDto? item)
    {
        if (item == null) return "Empty";
        if (Enum.TryParse<ItemRarity>(item.Rarity, out var r))
            return ViewUtils.RarityToColor(r) + item.Name + ViewUtils.YellowCode;
        return item.Name;
    }
}
