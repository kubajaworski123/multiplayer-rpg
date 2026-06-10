using System.Collections.Generic;

namespace MazeGame.Model.Items.Slots
{
    public sealed class Handle(string name = "Grip", char symbol = 'g', int slotCount = 2,
                  IEnumerable<StatBonus>? ownBonuses = null) : SlottedItem(name, symbol, slotCount, ownBonuses)
    {
        public override string Kind => "Handle";
    }
}
