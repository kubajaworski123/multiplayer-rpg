using MazeGame.Model.Characters;
using MazeGame.Model.Characters.Behaviors;
using MazeGame.Model.Characters.PlayerComponents;
using MazeGame.Model.Combat;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Items;
using MazeGame.Model.Items.Slots;
using MazeGame.Model.Weapons;
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
    public override CharacterType Type { get; protected set; } = CharacterType.Player;
    public bool hasWeapons;
    public IAttackVisitor? CurrentAttackStyle { get; set; }
    public int CurrentDefense { get; private set; } = 0;
    private readonly List<StatBonus> _equippedBonuses = new();
    public void RefreshEquippedBonuses()
    {
        foreach (StatBonus bonus in _equippedBonuses)
        {
            bonus.RevertBonus(Stats);
        }

        _equippedBonuses.Clear();

        LeftHand?.CollectStatBonuses(_equippedBonuses);
        if (RightHand != null && !ReferenceEquals(RightHand, LeftHand))
        {
            RightHand.CollectStatBonuses(_equippedBonuses);
        }

        foreach (StatBonus bonus in _equippedBonuses)
        {
            bonus.ApplyBonus(Stats);
        }
    }
        
    public void Attack(ICombat target)
    {
        if (CurrentAttackStyle == null) return;

        CurrentDefense = 0;
        if (LeftHand == null && RightHand == null)
        {
            Item fist = new Item("Fist", ' ');
            fist.Accept(CurrentAttackStyle, fist);
            target.ReciveDamage(CurrentAttackStyle.CalculatedDamageToEnemy, fromPlayer: true);
            CurrentDefense = CurrentAttackStyle.CalculatedPlayerDefense;
            return;
        }

        if (LeftHand != null && LeftHand == RightHand)
        {
            LeftHand.Accept(CurrentAttackStyle, LeftHand);
            target.ReciveDamage(CurrentAttackStyle.CalculatedDamageToEnemy, fromPlayer: true);
            CurrentDefense = CurrentAttackStyle.CalculatedPlayerDefense;
            return;
        }

        if (LeftHand != null)
        {
            LeftHand.Accept(CurrentAttackStyle, LeftHand);
            target.ReciveDamage(CurrentAttackStyle.CalculatedDamageToEnemy, fromPlayer: true);
            CurrentDefense = Math.Max(CurrentDefense, CurrentAttackStyle.CalculatedPlayerDefense);
        }

        if (RightHand != null)
        {
            RightHand.Accept(CurrentAttackStyle, RightHand);
            target.ReciveDamage(CurrentAttackStyle.CalculatedDamageToEnemy, fromPlayer: true);
            CurrentDefense = Math.Max(CurrentDefense, CurrentAttackStyle.CalculatedPlayerDefense);
        }
    }
    public override void ModifyHealth(int amount)
    {
        base.ModifyHealth(amount);
        Stats.ModifyHealth(amount);
    }

    public void ReciveDamage(int damageAmount, bool fromPlayer = false)
    {
        int realDamage = damageAmount - CurrentDefense;

        if (realDamage > 0)
        {
            ModifyHealth(-realDamage);
        }
    }

    public (int x, int y)? AttackPosition => null;

    public void ClearPendingAttack()
    {
    }
    public void SetName(string name)
    {
        this.Name = name;
    }
    public void Move(Map map, int x, int y)
    {
    }

    public bool Living => false;
    public bool BlocksSpawnTile => false;
    public virtual int? NetworkPlayerId => null;

    public void FindPosAndMove(MovementContext ctx)
    {
    }
}
