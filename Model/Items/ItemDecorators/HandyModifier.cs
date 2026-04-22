using MazeGame.Model.Dungeon;
using MazeGame.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items.ItemDecorators
{
    public class HandyModifier : ItemDecorator
    {
        public HandyModifier(Item decoratedItem) : base(decoratedItem) { }

        public override string Name => _decoratedItem.Name + " (Handy)";

        public override bool PickUp(Player player)
        {
            bool picked = base.PickUp(player);
            if (picked) 
                player.Stats.ModifyAgility(3);
            return picked;
        }

        public override void Drop(Player player, Map map)
        {
            base.Drop(player, map);
            player.Stats.ModifyAgility(-3);
        }
    }
}
