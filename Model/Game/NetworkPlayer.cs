using MazeGame.Model.Characters;
using MazeGame.Model.Players;

namespace MazeGame.Model.Game;

public class NetworkPlayer : Player
{
    public int PlayerId { get; set; }
    public bool IsConnected { get; set; } = true;

    public NetworkPlayer(int playerId)
    {
        PlayerId = playerId;
        Symbol = (char)('0' + playerId);
        SetType(CharacterType.Player);
    }

    public override int? NetworkPlayerId => PlayerId;
}
