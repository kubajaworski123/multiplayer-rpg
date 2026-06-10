using System.Collections.Generic;

namespace MazeGame.Model.Items.Slots
{
    public sealed class ItemSlots
    {
        private readonly Item?[] _slots;

        public ItemSlots(int count)
        {
            _slots = new Item?[count];
        }

        public int Count => _slots.Length;
        public IReadOnlyList<Item?> Children => _slots;

        public bool AddAt(int index, Item item)
        {
            if (index < 0 || index >= _slots.Length || _slots[index] != null)
            {
                return false;
            }
            _slots[index] = item;
            return true;
        }

        public bool Add(Item item)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == null)
                {
                    _slots[i] = item;
                    return true;
                }
            }
            return false;
        }

        public void CollectStatBonuses(List<StatBonus> bonuses)
        {
            foreach (Item? child in _slots)
            {
                child?.CollectStatBonuses(bonuses);
            }
        }

        public string Describe()
        {
            var parts = new List<string>(_slots.Length);
            foreach (Item? child in _slots)
            {
                parts.Add(child?.Name ?? "-");
            }
            return "[" + string.Join(", ", parts) + "]";
        }
    }
}
