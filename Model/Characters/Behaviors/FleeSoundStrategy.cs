namespace MazeGame.Model.Characters.Behaviors;

public sealed class FleeSoundStrategy : IMovementStrategy
{
    public bool TryMove(Mob mob, MovementContext ctx)
    {
        if (mob.LastHeardSound is not { } sound)
            return false;

        return MovementUtils.TryMoveAwayFrom(mob, ctx, sound.x, sound.y);
    }
}
