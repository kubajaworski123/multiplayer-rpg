namespace MazeGame.Networking.Dto;

public class CombatDto
{
    public int PlayerId { get; set; }
    public string LeftName { get; set; } = "";
    public string RightName { get; set; } = "";
    public int LeftHealth { get; set; }
    public int RightHealth { get; set; }
    public char LeftSymbol { get; set; }
    public char RightSymbol { get; set; }
    public string LeftType { get; set; } = "";
    public string RightType { get; set; } = "";
    public bool ActiveCombat { get; set; }
    public bool Turn { get; set; }
    public int HighlightedAttack { get; set; }
    public List<string> AttackNames { get; set; } = new();
    public string Outcome { get; set; } = "InProgress";
}
