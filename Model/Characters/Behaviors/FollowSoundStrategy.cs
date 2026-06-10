namespace MazeGame.Model.Characters.Behaviors;

public sealed class FollowSoundStrategy : IMovementStrategy
{
    public bool TryMove(Mob mob, MovementContext ctx)
    {
        if (mob.LastHeardSound is not { } sound)
            return false;

        if (mob.X == sound.x && mob.Y == sound.y)
        {
            mob.ClearLastHeardSound();
            return false;
        }

        return MovementUtils.TryMoveToward(mob, ctx, sound.x, sound.y);
    }
}
