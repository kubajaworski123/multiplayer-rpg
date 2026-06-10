namespace MazeGame.Model.Characters.Behaviors;

public interface IMovementStrategy
{
  bool TryMove(Mob mob, MovementContext ctx);
}
