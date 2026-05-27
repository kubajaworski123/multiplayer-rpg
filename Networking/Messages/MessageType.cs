namespace MazeGame.Networking.Messages;

public enum MessageType
{
    Join,
    JoinAccepted,
    Action,
    StateUpdate,
    Error,
    Disconnect
}
