using MazeGame.Model.Characters;
using MazeGame.Model.Characters.Behaviors;
using MazeGame.Model.Dungeon;

namespace MazeGame.Model.Combat
{
    public interface ICombat
    {
        int Health { get; }
        string Name { get; }
        CharacterType Type { get; }
        int X { get; }
        int Y { get; }
        char Symbol { get; }
        bool Living { get; }
        bool BlocksSpawnTile { get; }
        int? NetworkPlayerId { get; }
        (int x, int y)? AttackPosition { get; }
        void ReciveDamage(int damage, bool fromPlayer = false);
        void Attack(ICombat enemy);
        void SetType(CharacterType type);
        void Move(Map map, int x, int y);
        void FindPosAndMove(MovementContext ctx);
        void ClearPendingAttack();
    }
}
