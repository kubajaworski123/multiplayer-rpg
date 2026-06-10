namespace MazeGame.Model.Characters.Behaviors;

public sealed class FleePlayersStrategy : IMovementStrategy
{
    public bool TryMove(Mob mob, MovementContext ctx)
    {
        var visible = MovementUtils.GetVisiblePlayers(mob, ctx);
        if (visible.Count == 0)
            return false;

        if (MovementUtils.TryFleeFromAll(mob, ctx, visible))
            return true;

        return true;
    }
}
