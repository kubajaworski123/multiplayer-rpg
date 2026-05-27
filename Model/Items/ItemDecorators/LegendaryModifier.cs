using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items.ItemDecorators
{
    public class LegendaryModifier : ItemDecorator
    {
        public LegendaryModifier(Item decoratedItem) : base(decoratedItem) { }
        public override ItemRarity Rarity => ItemRarity.Legendary;
        public override string Name => $"Legendary " + _decoratedItem.Name;
        public override int Damage => _decoratedItem.Damage + 5;

        protected override void AppendDecoratorTags(List<string> tags) => tags.Add("Legendary");
    }
}
