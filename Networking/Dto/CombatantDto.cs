namespace MazeGame.Networking.Dto;

public class CombatantDto
{
    public string Name { get; set; } = "";
    public char Symbol { get; set; }
    public string Type { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }
    public int Health { get; set; }
    public bool IsPlayer { get; set; }
    public int? PlayerId { get; set; }
}
