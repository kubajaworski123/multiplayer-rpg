namespace MazeGame.Networking.Dto;

public class TileDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsWall { get; set; }
    public char Symbol { get; set; }
    public List<ItemDto> Entities { get; set; } = new();
}
