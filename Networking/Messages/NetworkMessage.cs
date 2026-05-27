using MazeGame.Model.Game;
using MazeGame.Networking.Dto;

namespace MazeGame.Networking.Messages;

public class NetworkMessage
{
    public MessageType Type { get; set; }
    public string? PlayerName { get; set; }
    public int PlayerId { get; set; }
    public PlayerAction? Action { get; set; }
    public GameStateDto? State { get; set; }
    public string? Error { get; set; }
}
