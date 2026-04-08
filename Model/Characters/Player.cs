using MazeGame.Model.Characters;
using MazeGame.Model.Characters.PlayerComponents;
using MazeGame.Model.Combat;
using MazeGame.Model.Items;
using MazeGame.Model.Weapons;
using MazeGame.View;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MazeGame.Model.Players;
public class Player : Character, ICombat
{
    public Attributes Stats { get; set; } = new Attributes();
    public Backpack Backpack { get; set; } = new Backpack();
    public Item? LeftHand { get; set; }
    public Item? RightHand { get; set; }
    public override string ColorCode => "\x1b[36m";
    public bool hasWeapons;
    public IAttackVisitor? CurrentAttackStyle { get; set; }
    public int CurrentDefense { get; private set; } = 0;

    public void Attack(ICombat target)
    {
        if (CurrentAttackStyle == null) return;

        CurrentDefense = 0;
        if (LeftHand == null && RightHand == null)
        {
            Item fist = new Item("Fist", ' ');
            fist.Accept(CurrentAttackStyle, fist);
            target.ReciveDamage(CurrentAttackStyle.CalculatedDamageToEnemy);
            CurrentDefense = CurrentAttackStyle.CalculatedPlayerDefense;
            return;
        }

        if (LeftHand != null && LeftHand == RightHand)
        {
            LeftHand.Accept(CurrentAttackStyle, LeftHand);
            target.ReciveDamage(CurrentAttackStyle.CalculatedDamageToEnemy);
            CurrentDefense = CurrentAttackStyle.CalculatedPlayerDefense;
            return;
        }

        if (LeftHand != null)
        {
            LeftHand.Accept(CurrentAttackStyle, LeftHand);
            target.ReciveDamage(CurrentAttackStyle.CalculatedDamageToEnemy);
            CurrentDefense = Math.Max(CurrentDefense, CurrentAttackStyle.CalculatedPlayerDefense);
        }

        if (RightHand != null)
        {
            RightHand.Accept(CurrentAttackStyle, RightHand);
            target.ReciveDamage(CurrentAttackStyle.CalculatedDamageToEnemy);
            CurrentDefense = Math.Max(CurrentDefense, CurrentAttackStyle.CalculatedPlayerDefense);
        }
    }
    public void ReciveDamage(int damageAmount)
    {
        int realDamage = damageAmount - CurrentDefense;

        if (realDamage > 0)
        {
            ModifyHealth(-realDamage);
        }
    }
}
