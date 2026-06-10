using System.Collections.Generic;

namespace MazeGame.Model.Items.Slots
{
    public abstract class SlottedItem : Item, ISlotContainer
    {
        protected readonly ItemSlots Slots;
        protected readonly List<StatBonus> OwnBonuses;

        protected SlottedItem(string name, char symbol, int slotCount, IEnumerable<StatBonus>? ownBonuses = null)
            : base(name, symbol)
        {
            Slots = new ItemSlots(slotCount);
            OwnBonuses = ownBonuses != null ? new List<StatBonus>(ownBonuses) : new List<StatBonus>();
        }

        public int SlotCount => Slots.Count;
        public IReadOnlyList<Item?> SlotChildren => Slots.Children;
        public bool PlaceInSlot(int index, Item item)
        {
            return Slots.AddAt(index, item);
        }
        public bool AddToSlot(Item item)
        {
            return Slots.Add(item);
        }

        public override string Name => $"{base.Name} {Slots.Describe()}";

        public override void CollectStatBonuses(List<StatBonus> bonuses)
        {
            foreach (StatBonus bonus in OwnBonuses)
            {
                bonuses.Add(bonus);
            }

            Slots.CollectStatBonuses(bonuses);
        }

        public override string ToString() => Name;
    }
}
