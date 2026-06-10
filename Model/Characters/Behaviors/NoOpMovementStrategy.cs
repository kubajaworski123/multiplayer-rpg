namespace MazeGame.Model.Characters.Behaviors;

public sealed class NoOpMovementStrategy : IMovementStrategy
{
    public bool TryMove(Mob mob, MovementContext ctx) => false;
}
