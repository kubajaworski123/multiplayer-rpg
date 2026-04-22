using MazeGame.Controller;
using MazeGame.Model.Characters;
using MazeGame.Model.Observers;
using MazeGame.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Combat
{
    public class CombatSession(ICombat leftActor, ICombat rightActor, List<IAttackVisitor> attacks, LogBus logBus)
    {
        public ICombat LeftActor { get; private set; } = leftActor;
        public ICombat RightActor  { get; private set; }  = rightActor;
        public bool Turn { get; private set; } = true;
        public bool ActiveCombat { get; private set; } = true;
        private readonly LogBus _logBus = logBus;
        public int HighlightedAttackOtption { get; private set; } = 0;
        public List<IAttackVisitor> PossibleAttacks { get; private set; } = attacks;
        public int PossibleAttackOtptions => PossibleAttacks.Count ;

        public void PlayersTurn()
        {
            if (Turn == false || !ActiveCombat)
                return;

            int hpBefore = RightActor.Health;
            LeftActor.Attack(RightActor);
            int damageDealt = hpBefore - RightActor.Health;
            _logBus.Publish($"{LeftActor.Name} attacks {RightActor.Name} for {damageDealt} dmg");
            if (RightActor.Health <= 0)
            {
                _logBus.Publish($"{RightActor.Name} is defeated");
                ActiveCombat = false;
            }
            else
            {
                SwitchTurn();
                MobAttacs();
            }
        }
        public void MobAttacs()
        {
            if (Turn == true || !ActiveCombat)
                return;

            int hpBefore = LeftActor.Health;
            RightActor.Attack(LeftActor);
            int damageDealt = hpBefore - LeftActor.Health;
            _logBus.Publish($"{RightActor.Name} attacks {LeftActor.Name} for {damageDealt} dmg");
            if (LeftActor.Health <= 0)
            {
                _logBus.Publish($"{RightActor.Name} is defeated");
                ActiveCombat = false;   
            }
            else
            {
                SwitchTurn();
            }
        }
        public void SwitchTurn() 
        {
                Turn = !Turn; 
        }
        public void ScrollAtacksDown()
        {
            HighlightedAttackOtption = (HighlightedAttackOtption + 1) % PossibleAttackOtptions;
        }
        public void ScrollAttacksUp()
        {
            HighlightedAttackOtption = HighlightedAttackOtption == 0 ? PossibleAttackOtptions - 1 : HighlightedAttackOtption - 1;
        }
        
    }
}
