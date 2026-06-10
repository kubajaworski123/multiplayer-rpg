using MazeGame.Model.Dungeon;

namespace MazeGame.Model.Characters.Behaviors;

public readonly struct PlayerSpot(int x, int y, int? playerId = null)
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public int? PlayerId { get; } = playerId;
}

public sealed class MovementContext
{
    public required Map Map { get; init; }
    public required HashSet<(int x, int y)> BlockedPositions { get; init; }
    public required IReadOnlyList<PlayerSpot> Players { get; init; }
    public int VisionRange { get; init; } = 5;
}
