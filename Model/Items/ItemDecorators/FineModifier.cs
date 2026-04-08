using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Items.ItemDecorators
{
    public class FineModifier : ItemDecorator
    {
        public string ColorCode { get; private set; } = "\x1b[34m";

        public FineModifier(Item decoratedItem) : base(decoratedItem) { }

        public override string Name => $"{ColorCode}Fine\x1b[93m " + _decoratedItem.Name;

        public override int Damage => _decoratedItem.Damage + 2;
    }
}
