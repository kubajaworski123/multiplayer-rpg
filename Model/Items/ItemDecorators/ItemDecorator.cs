using MazeGame.Model.Dungeon;
using MazeGame.Model.Items.ItemDecorators;
using MazeGame.Model.Players;

namespace MazeGame.Model.Items.ItemDecorators
{
    public abstract class ItemDecorator : Item
    {
        protected Item _decoratedItem;
        public Item DecoratedItem => _decoratedItem;
        public override ItemRarity Rarity => _decoratedItem.Rarity;
        public override bool IsTwoHanded => _decoratedItem.IsTwoHanded;
        public override int NoiseRange => _decoratedItem.NoiseRange;
        public ItemDecorator(Item decoratedItem) : base(decoratedItem.Name, decoratedItem.Symbol)
        {
            _decoratedItem = decoratedItem;
        }

        public override int Damage => _decoratedItem.Damage;
        public override string Name => _decoratedItem.Name;

        public override void Accept(Combat.IAttackVisitor visitor, Item context)
        {
            _decoratedItem.Accept(visitor, context);
        }

        public override bool PickUp(Player player)
        {
            bool picked = _decoratedItem.PickUp(player);
            if (picked)
            {
                player.Backpack.RemoveFromInventory(_decoratedItem);
                player.Backpack.AddToInventory(this);
            }
            return picked;
        }

        public override void Drop(Player player, Map map)
        {
            _decoratedItem.Drop(player, map);
            map[player.X, player.Y].RemoveMapEntity(_decoratedItem);
            map[player.X, player.Y].AddMapEntity(this);
            player.Backpack.RemoveFromInventory(this);
        }

        public override void Equip(Player player, Map map)
        {
            _decoratedItem.Equip(player, map);
            if (player.LeftHand == _decoratedItem)
                player.LeftHand = this;
            if (player.RightHand == _decoratedItem) 
                player.RightHand = this;
            player.Backpack.RemoveFromInventory(this);
        }

        public override void CollectModifierTags(List<string> tags)
        {
            AppendDecoratorTags(tags);
            _decoratedItem.CollectModifierTags(tags);
        }

        protected virtual void AppendDecoratorTags(List<string> tags)
        {
        }
    }
}