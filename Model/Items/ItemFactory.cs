using MazeGame.Model.Items.ItemDecorators;
using MazeGame.Model.Weapons;
using System;
using System.Collections.Generic;

namespace MazeGame.Model.Items
{
    public static class ItemFactory
    {
        private static readonly Random _random = new Random();

        public static Item CreateRandomDecoratedItem()
        {
            Item baseItem = ItemsList.GetRandomItem();
            return ApplyRandomModifiers(baseItem);
        }
        public static Item CreateRandomDecoratedWeapon()
        {
            Weapon baseWeapon = WeaponLists.GetRandomWeapon();
            return ItemFactory.ApplyRandomModifiers(baseWeapon);
        }
        public static Item ApplyRandomModifiers(Item item)
        {
            int modifiersToApply = _random.Next(0, 3);
            List<int> availableModifiers = new List<int> { 0, 1, 2, 3 };

            for (int i = 0; i < modifiersToApply; i++)
            {
                int listIndex = _random.Next(availableModifiers.Count);
                int modifierId = availableModifiers[listIndex];

                item = ApplyModifier(item, modifierId);

                availableModifiers.RemoveAt(listIndex);
            }

            return item;
        }

        private static Item ApplyModifier(Item item, int modifierId)
        {
            return modifierId switch
            {
                0 => new FineModifier(item),
                1 => new LegendaryModifier(item),
                2 => new LuckyModifier(item),
                3 => new HandyModifier(item),
                _ => item
            };
        }
    }
}