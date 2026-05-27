namespace MazeGame.Networking.Dto;

public class MapDto
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<TileDto> Tiles { get; set; } = new();
    public List<CombatantDto> Combatants { get; set; } = new();
}
