using System.Collections.Generic;
using MazeGame.Model.Items.ItemDecorators;
using MazeGame.Model.Weapons;

namespace MazeGame.Model.Items.Slots
{
    public sealed class SlottedWeapon : ItemDecorator, ISlotContainer
    {
        private readonly ItemSlots _slots;
        private readonly List<StatBonus> _ownBonuses;

        public SlottedWeapon(Weapon weapon, int slotCount, IEnumerable<StatBonus>? ownBonuses = null)
            : base(weapon)
        {
            _slots = new ItemSlots(slotCount);
            _ownBonuses = ownBonuses != null ? new List<StatBonus>(ownBonuses) : new List<StatBonus>();
        }

        public int SlotCount => _slots.Count;
        public IReadOnlyList<Item?> SlotChildren => _slots.Children;
        public bool PlaceInSlot(int index, Item item) => _slots.AddAt(index, item);
        public bool AddToSlot(Item item) => _slots.Add(item);

        public override string Kind => _decoratedItem.Kind;
        public override string Name => $"{_decoratedItem.Name} {_slots.Describe()}";

        public override void CollectStatBonuses(List<StatBonus> bonuses)
        {
            foreach (StatBonus bonus in _ownBonuses)
            {
                bonuses.Add(bonus);
            }
            _slots.CollectStatBonuses(bonuses);
        }
    }
}
