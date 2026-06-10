namespace MazeGame.Model.Characters.Behaviors;

public sealed class MobMovementPlanner
{
    private readonly IMovementStrategy _vision;
    private readonly IMovementStrategy _sound;
    private readonly IMovementStrategy _default;

    public MobMovementPlanner(
        IMovementStrategy vision,
        IMovementStrategy sound,
        IMovementStrategy defaultStrategy)
    {
        _vision = vision;
        _sound = sound;
        _default = defaultStrategy;
    }

    public bool Execute(Mob mob, MovementContext ctx)
    {
        mob.ClearPendingAttack();
        if (_vision.TryMove(mob, ctx))
        {
            return true;
        }

        if (_sound.TryMove(mob, ctx))
        {
            return true;
        }

        return _default.TryMove(mob, ctx);
    }
}
