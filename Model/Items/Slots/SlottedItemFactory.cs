using MazeGame.Model.Weapons;

namespace MazeGame.Model.Items.Slots
{
    public static class SlottedItemFactory
    {
        public static SlottedWeapon CreateSlottedSword()
        {
            var sword = new SlottedWeapon(
                new HeavyWeapon("SW", '?', 18, false),
                slotCount: 2,
                ownBonuses: new[] { StatBonus.Strength(2) });

            sword.PlaceInSlot(0, new PassiveItem("s1", 'o', StatBonus.Luck(2)));

            var handle = new Handle("g1", 'h', 2);
            handle.PlaceInSlot(0, new PassiveItem("s2", 'o', StatBonus.Strength(2)));

            var handle2 = new Handle("g2", 'h', 2);
            handle2.PlaceInSlot(1, new PassiveItem("s3", 'o', StatBonus.Wisdom(2)));
            handle.PlaceInSlot(1, handle2);

            sword.PlaceInSlot(1, handle);
            return sword;
        }

        public static SlottedWeapon CreateSlottedBlaster()
        {
            var blaster = new SlottedWeapon(
                new LightWeapon("Slotted Blaster", 'z', 9, false),
                slotCount: 2);
            blaster.PlaceInSlot(0, new PassiveItem("Quick Stone", 'o', StatBonus.Agility(2)));
            return blaster;
        }

        public static Handle CreateLoadedHandle()
        {
            var handle = new Handle("Grip", 'h', 2);
            handle.PlaceInSlot(0, new PassiveItem("Wise Stone", 'o', StatBonus.Wisdom(2)));
            return handle;
        }

        public static PassiveItem CreateLuckyStone() =>
            new PassiveItem("Lucky Stone", 'o', StatBonus.Luck(2));
    }
}
