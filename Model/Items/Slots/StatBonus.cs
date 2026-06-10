using System;
using MazeGame.Model.Characters.PlayerComponents;

namespace MazeGame.Model.Items.Slots
{
    public sealed class StatBonus(Action<Attributes, int> modify, int amount, string code)
    {
        private readonly Action<Attributes, int> _modify = modify;
        private readonly int _amount = amount;
        public string Code { get; } = code;

        public void ApplyBonus(Attributes stats) => _modify(stats, _amount);
        public void RevertBonus(Attributes stats) => _modify(stats, -_amount);

        public string Label => $"+{_amount}{Code}";

        public static StatBonus Strength(int amount) => new((s, a) => s.ModifyStrength(a), amount, "S");
        public static StatBonus Agility(int amount) => new((s, a) => s.ModifyAgility(a), amount, "A");
        public static StatBonus Wisdom(int amount) => new((s, a) => s.ModifyWisdom(a), amount, "W");
        public static StatBonus Happiness(int amount) => new((s, a) => s.ModifyHappiness(a), amount, "H");
        public static StatBonus Aggression(int amount) => new((s, a) => s.ModifyAgressiveness(a), amount, "R");
        public static StatBonus Luck(int amount) => new((s, a) => s.ModifyLuck(a), amount, "L");
    }
}
