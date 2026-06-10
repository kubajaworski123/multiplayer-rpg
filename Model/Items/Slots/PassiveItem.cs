using System.Collections.Generic;

namespace MazeGame.Model.Items.Slots
{
    public sealed class PassiveItem(string name, char symbol, StatBonus bonus) : Item(name, symbol)
    {
        private readonly StatBonus _bonus = bonus;

        public override string Kind => "PassiveItem";
        public override string Name => $"{base.Name} {_bonus.Label}";

        public override void CollectStatBonuses(List<StatBonus> bonuses)
        {
            bonuses.Add(_bonus);
        }

        public override string ToString() => Name;
    }
}
