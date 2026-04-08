using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items.ItemDecorators
{
    public class LegendaryModifier : ItemDecorator
    {
        public string ColorCode { get; private set; } = "\x1b[31m";

        public LegendaryModifier(Item decoratedItem) : base(decoratedItem) { }

        public override string Name => $"{ColorCode}Legendary\x1b[93m " + _decoratedItem.Name;

        public override int Damage => _decoratedItem.Damage + 5;
    }
}
