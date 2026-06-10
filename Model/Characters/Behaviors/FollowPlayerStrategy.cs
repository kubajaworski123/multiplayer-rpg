namespace MazeGame.Model.Characters.Behaviors;

public sealed class FollowPlayerStrategy : IMovementStrategy
{
    public bool TryMove(Mob mob, MovementContext ctx)
    {
        var visible = MovementUtils.GetVisiblePlayers(mob, ctx);
        if (visible.Count == 0)
            return false;

        mob.ClearLastHeardSound();

        var target = MovementUtils.FindNearest(mob, visible);
        if (target == null)
            return false;

        int dist = Math.Abs(mob.X - target.Value.X) + Math.Abs(mob.Y - target.Value.Y);
        if (dist <= 1)
        {
            mob.SetPendingAttack(target.Value.X, target.Value.Y);
            return true;
        }

        return MovementUtils.TryMoveToward(mob, ctx, target.Value.X, target.Value.Y);
    }
}
