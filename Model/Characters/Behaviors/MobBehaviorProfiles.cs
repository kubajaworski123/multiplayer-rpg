namespace MazeGame.Model.Characters.Behaviors;

public static class MobBehaviorProfiles
{
    public static MobMovementPlanner Aggressive() =>
        new(new FollowPlayerStrategy(), new FollowSoundStrategy(), new RandomMovementStrategy());

    public static MobMovementPlanner Cowardly() =>
        new(new FleePlayersStrategy(), new FleeSoundStrategy(), new RandomMovementStrategy());

    public static MobMovementPlanner Neutral() =>
        new(new NoOpMovementStrategy(), new NoOpMovementStrategy(), new RandomMovementStrategy());
}
