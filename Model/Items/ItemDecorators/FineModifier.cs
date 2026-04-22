using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items.ItemDecorators
{
    public class FineModifier : ItemDecorator
    {
        public FineModifier(Item decoratedItem) : base(decoratedItem) { }

        public override string Name => $"Fine " + _decoratedItem.Name;

        public override int Damage => _decoratedItem.Damage + 2;
        public override ItemRarity Rarity => ItemRarity.Fine;
    }
}
