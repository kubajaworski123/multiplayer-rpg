using MazeGame.Model.Dungeon;
using MazeGame.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items.ItemDecorators
{
    public class LuckyModifier : ItemDecorator
    {
        public LuckyModifier(Item decoratedItem) : base(decoratedItem) { }

        public override string Name => _decoratedItem.Name + " (Lucky)";

        public override void PickUp(Player player)
        {
            base.PickUp(player);
            player.Stats.ModifyLuck(5);
        }
        
        public override void Drop(Player player, Map map)
        {
            base.Drop(player, map);
            player.Stats.ModifyLuck(-5);
        }
    }
}
