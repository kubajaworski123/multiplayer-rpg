using MazeGame.Model.Characters.Behaviors;
using MazeGame.Model.Combat;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Observers;
using MazeGame.Model.SoundPropagation;

namespace MazeGame.Model.Characters
{
    public abstract class Mob : Character, ICombat, IMobSubscriber, ISoundSubscriber
    {
        public int Damage { get; protected set; }
        public int DamageVariation => Math.Max(1, Damage / 3);
        public int Armour { get; protected set; }

        protected IMobPublisher? _mobGroup;

        private readonly int _maxHealth;
        private MobMovementPlanner _planner;
        private bool _isNeutral;
        private bool _wasAttackedByPlayer;

        public (int x, int y)? LastHeardSound { get; private set; }
        public (int x, int y)? AttackPosition { get; private set; }

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
            _maxHealth = health;
            _planner = MobBehaviorProfiles.Neutral();
            _mobGroup = mobGroup;

            _mobGroup?.Subscribe(this);
            SoundPublisher.Instance.Subscribe(this);
        }

        public void ConfigureMovement(MobMovementPlanner planner)
        {
            _planner = planner;
            _isNeutral = false;
        }

        public void ConfigureAsNeutral()
        {
            _isNeutral = true;
            _wasAttackedByPlayer = false;
            _planner = MobBehaviorProfiles.Neutral();
        }

        public void SetPendingAttack(int x, int y) => AttackPosition = (x, y);

        public void ClearPendingAttack() => AttackPosition = null;

        public void ClearLastHeardSound() => LastHeardSound = null;

        public void Attack(ICombat target)
        {
            target.ReciveDamage(Damage);
        }

        public void ReciveDamage(int damage, bool fromPlayer = false)
        {
            int realDamage = damage - Armour;
            if (realDamage > 0)
            {
                ModifyHealth(-realDamage);
                if (fromPlayer && _isNeutral && !_wasAttackedByPlayer)
                {
                    _wasAttackedByPlayer = true;
                    _planner = Health * 2 >= _maxHealth
                        ? MobBehaviorProfiles.Aggressive()
                        : MobBehaviorProfiles.Cowardly();
                }
                if (Health <= 0)
                    ApplyDeathEffects();
            }
        }

        protected virtual void ApplyDeathEffects()
        {
            _mobGroup?.NotifyDeath(this);
            LogBus.Logs.Publish($"Stats for some enemies have changed due to your actions");
            SoundPublisher.Instance.Unsubscribe(this);
        }

        public void OnSound(SoundData data)
        {
            if (data.SourceName is not ("pickup" or "drop"))
                return;

            int distance = data.MapContext.GetPathDistance(X, Y, data.SourceX, data.SourceY);
            if (distance != -1 && distance <= data.Range)
            {
                LastHeardSound = (data.SourceX, data.SourceY);
                LogBus.Logs.Publish($"{Name} on ({X},{Y}) heard {data.SourceName} from dist. {distance}");
            }
        }

        public abstract void OnFriendsDeath();

        public bool Living => Type != CharacterType.Dead;
        public bool BlocksSpawnTile => Type != CharacterType.Dead;
        public int? NetworkPlayerId => null;

        public void Move(Map map, int x, int y) => Move(x, y);

        public void FindPosAndMove(MovementContext ctx)
        {
            if (Living)
            {
                _planner.Execute(this, ctx);
            }
        }
    }
}
