namespace MazeGame.Networking.Dto;

public class GameStateDto
{
    public int GameStateN { get; set; }
    public bool IsRunning { get; set; }
    public MapDto Map { get; set; } = new();
    public List<PlayerDto> Players { get; set; } = new();
    public List<CombatDto> Combats { get; set; } = new();
    public List<string> RecentLogs { get; set; } = new();
    public List<string> AllLogs { get; set; } = new();
    public List<string> UsersManual { get; set; } = new();
    public List<string> Controls { get; set; } = new();
}
