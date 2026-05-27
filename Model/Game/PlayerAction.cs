namespace MazeGame.Model.Game;

public class PlayerAction
{
    public int PlayerId { get; set; }
    public PlayerActionType Type { get; set; }
    public int XMove { get; set; }
    public int YMove { get; set; }
    public int Idx { get; set; }
}
