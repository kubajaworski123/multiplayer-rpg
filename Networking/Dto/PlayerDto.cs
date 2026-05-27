namespace MazeGame.Networking.Dto;

public class PlayerDto
{
    public int Id { get; set; }
    public char Symbol { get; set; }
    public string Name { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }
    public int Health { get; set; }
    public StatsDto Stats { get; set; } = new();
    public BackpackDto Backpack { get; set; } = new();
    public ItemDto? LeftHand { get; set; }
    public ItemDto? RightHand { get; set; }
    public bool IsConnected { get; set; }
}
