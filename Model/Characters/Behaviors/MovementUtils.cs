using MazeGame.Model.Dungeon;

namespace MazeGame.Model.Characters.Behaviors;

internal static class MovementUtils
{
    private static readonly (int dx, int dy)[] Directions =
    [
        (0, -1), (0, 1), (-1, 0), (1, 0), (0, 0)
    ];

    public static List<(int x, int y)> GetValidMoves(Mob mob, MovementContext ctx)
    {
        var valid = new List<(int x, int y)>();
        foreach (var (dx, dy) in Directions)
        {
            int nx = mob.X + dx;
            int ny = mob.Y + dy;
            if (nx < 0 || nx >= ctx.Map.Width || ny < 0 || ny >= ctx.Map.Height)
                continue;
            if (ctx.Map.Tiles[nx, ny].IsWall)
                continue;
            if (ctx.BlockedPositions.Contains((nx, ny)))
                continue;
            valid.Add((nx, ny));
        }
        return valid;
    }

    public static bool TryMoveToward(Mob mob, MovementContext ctx, int targetX, int targetY)
    {
        var valid = GetValidMoves(mob, ctx);
        if (valid.Count == 0)
            return false;

        (int x, int y)? best = null;
        int bestDist = int.MaxValue;
        foreach (var move in valid)
        {
            if (move.x == mob.X && move.y == mob.Y)
                continue;
            int dist = Math.Abs(move.x - targetX) + Math.Abs(move.y - targetY);
            if (dist < bestDist)
            {
                bestDist = dist;
                best = move;
            }
        }

        if (best == null)
            return false;

        mob.Move(best.Value.x, best.Value.y);
        return true;
    }

    public static bool TryMoveAwayFrom(Mob mob, MovementContext ctx, int threatX, int threatY)
    {
        var valid = GetValidMoves(mob, ctx);
        if (valid.Count == 0)
            return false;

        int currentDist = Math.Abs(mob.X - threatX) + Math.Abs(mob.Y - threatY);
        var improving = valid
            .Where(m => m.x != mob.X || m.y != mob.Y)
            .Where(m => Math.Abs(m.x - threatX) + Math.Abs(m.y - threatY) > currentDist)
            .ToList();

        if (improving.Count == 0)
            return false;

        var pick = improving[Random.Shared.Next(improving.Count)];
        mob.Move(pick.x, pick.y);
        return true;
    }

    public static bool TryFleeFromAll(Mob mob, MovementContext ctx, IReadOnlyList<PlayerSpot> threats)
    {
        var valid = GetValidMoves(mob, ctx);
        if (valid.Count == 0)
            return false;

        var nonStay = valid.Where(m => m.x != mob.X || m.y != mob.Y).ToList();
        if (nonStay.Count == 0)
            return true;

        var safe = nonStay.Where(move =>
        {
            foreach (var threat in threats)
            {
                int newDist = Math.Abs(move.x - threat.X) + Math.Abs(move.y - threat.Y);
                int oldDist = Math.Abs(mob.X - threat.X) + Math.Abs(mob.Y - threat.Y);
                if (newDist <= oldDist)
                    return false;
            }
            return true;
        }).ToList();

        if (safe.Count == 0)
            return false;

        var pick = safe[Random.Shared.Next(safe.Count)];
        mob.Move(pick.x, pick.y);
        return true;
    }

    public static List<PlayerSpot> GetVisiblePlayers(Mob mob, MovementContext ctx)
    {
        var visible = new List<PlayerSpot>();
        foreach (var player in ctx.Players)
        {
            if (ctx.Map.HasLineOfSight(mob.X, mob.Y, player.X, player.Y, ctx.VisionRange))
                visible.Add(player);
        }
        return visible;
    }

    public static PlayerSpot? FindNearest(Mob mob, IReadOnlyList<PlayerSpot> players)
    {
        if (players.Count == 0)
            return null;

        PlayerSpot nearest = players[0];
        int best = Math.Abs(mob.X - nearest.X) + Math.Abs(mob.Y - nearest.Y);
        for (int i = 1; i < players.Count; i++)
        {
            int dist = Math.Abs(mob.X - players[i].X) + Math.Abs(mob.Y - players[i].Y);
            if (dist < best)
            {
                best = dist;
                nearest = players[i];
            }
        }
        return nearest;
    }
}
