using MazeGame.Controller;
using MazeGame.Model.Characters;
using MazeGame.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Combat
{
    public class CombatSession(ICombat leftActor, ICombat rightActor, List<IAttackVisitor> attacks)
    {
        public ICombat LeftActor { get; private set; } = leftActor;
        public ICombat RightActor  { get; private set; }  = rightActor;
        public bool Turn { get; private set; } = true;
        public bool ActiveCombat { get; private set; } = true;
        public int HighlightedAttackOtption { get; private set; } = 0;
        public List<IAttackVisitor> PossibleAttacks { get; private set; } = attacks;
        public int PossibleAttackOtptions => PossibleAttacks.Count ;

        public void PlayersTurn()
        {
            if (Turn == false || !ActiveCombat)
                return;

            //System.Threading.Thread.Sleep(1000);
            LeftActor.Attack(RightActor);
            if (RightActor.Health <= 0)
            {
                ActiveCombat = false;
            }
            else
            {
                SwitchTurn();
                //System.Threading.Thread.Sleep(1000);
                MobAttacs();
            }
        }
        public void MobAttacs()
        {
            if (Turn == true || !ActiveCombat)
                return;

            RightActor.Attack(LeftActor);
            if (LeftActor.Health <= 0)
            {
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
