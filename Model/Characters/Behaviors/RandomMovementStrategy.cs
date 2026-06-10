namespace MazeGame.Model.Characters.Behaviors;

public sealed class RandomMovementStrategy : IMovementStrategy
{
    public bool TryMove(Mob mob, MovementContext ctx)
    {
        var valid = MovementUtils.GetValidMoves(mob, ctx);
        if (valid.Count == 0)
            return false;

        var move = valid[Random.Shared.Next(valid.Count)];
        mob.Move(move.x, move.y);
        return true;
    }
}
