using MazeGame.Model.Combat;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Observers;
using MazeGame.Model.SoundPropagation;
using System;

namespace MazeGame.Model.Characters
{
    public abstract class Mob : Character, ICombat, IMobSubscriber, ISoundSubscriber
    {
        public int Damage { get; protected set; }
        public int DamageVariation => Math.Max(1, Damage / 3);
        public int Armour { get; protected set; }

        private Random rnd = new Random();

        protected IMobPublisher? _mobGroup;

        public Mob(
            string name = "unknown",
            char symbol = '!',
            int health = 100,
            int x = 1,
            int y = 1,
            CharacterType type = CharacterType.BountyHunter,
            int attack = 1,
            int armour = 1,
            IMobPublisher? mobGroup = null)
            : base(name, symbol, health, type, x, y)
        {
            Damage = attack;
            Armour = armour;
            _mobGroup = mobGroup;

            _mobGroup?.Subscribe(this);
            SoundPublisher.Instance.Subscribe(this);
        }

        public void Attack(ICombat target)
        {
            target.ReciveDamage(Damage);
        }

        public void ReciveDamage(int damage)
        {
            int realDamage = damage - Armour;
            if (realDamage > 0)
            {
                ModifyHealth(-realDamage);
                if (Health <= 0)
                {
                    ApplyDeathEffects();
                }
            }
        }
        protected virtual void ApplyDeathEffects()
        {
            _mobGroup?.NotifyDeath(this);
            LogBus.Logs.Publish($"Stats for some enemies have changed due to your actions");
            SoundPublisher.Instance.Unsubscribe(this);
        }
        public virtual void Move(Map map, int playerX, int playerY)
        {
            Move(map, new HashSet<(int, int)> { (playerX, playerY) });
        }

        public virtual void Move(Map map, HashSet<(int x, int y)> blockedPositions)
        {
            List<(int x, int y)> validMoves = new List<(int x, int y)>();

            (int x, int y)[] dirrections = new (int x, int y)[]
            {
                (0, -1),
                (0, 1), 
                (-1, 0),
                (1, 0), 
                (0, 0)  
            };

            for (int i = 0; i < 5; i++)
            {
                int newX = base.X + dirrections[i].x;
                int newY = base.Y + dirrections[i].y;

                if (newX >= 0 && newX < map.Width && newY >= 0 && newY < map.Height && !map.Tiles[newX, newY].IsWall)
                {
                    if (!blockedPositions.Contains((newX, newY)))
                    {
                        validMoves.Add((newX, newY));
                    }
                }
            }

            if (validMoves.Count > 0)
            {
                var move = validMoves[rnd.Next(validMoves.Count)];
                Move(move.x, move.y);
            }
        }
        public void OnSound(SoundData data)
        {
            int distance = data.MapContext.GetPathDistance(X, Y, data.SourceX, data.SourceY);

            if (distance != -1 && distance <= data.Range)
            {
                LogBus.Logs.Publish($"{Name} on ({X},{Y}) heard {data.SourceName} from dist. {distance}");
            }
        }
        public abstract void OnFriendsDeath();

        public bool Living => Type != CharacterType.Dead;
        public bool BlocksSpawnTile => Type != CharacterType.Dead;
        public int? NetworkPlayerId => null;

        public void FindPosAndMove(Map map, HashSet<(int x, int y)> blockedPositions)
        {
            if (Living)
                Move(map, blockedPositions);
        }
    }
}