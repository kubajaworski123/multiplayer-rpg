using System.Collections.Generic;

namespace MazeGame.Model.Items.Slots
{
    public interface ISlotContainer
    {
        int SlotCount { get; }
        IReadOnlyList<Item?> SlotChildren { get; }
        bool PlaceInSlot(int index, Item item);
        bool AddToSlot(Item item);
    }
}
