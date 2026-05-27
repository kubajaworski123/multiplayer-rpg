namespace MazeGame.Networking.Dto;

public class ItemDto
{
    public string Name { get; set; } = "";
    public char Symbol { get; set; }
    public string Kind { get; set; } = "Item";
    public string Rarity { get; set; } = "Common";
    public int Damage { get; set; }
    public bool IsTwoHanded { get; set; }
    public int NoiseRange { get; set; }
    public List<string> Modifiers { get; set; } = new();
}
