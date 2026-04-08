using MazeGame.Model.Dungeon;
using MazeGame.Model.Items.ItemDecorators;
using MazeGame.Model.Players;

namespace MazeGame.Model.Items.ItemDecorators
{
    public abstract class ItemDecorator : Item
    {
        protected Item _decoratedItem;

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

        public override void PickUp(Player player)
        {
            _decoratedItem.PickUp(player);
            player.Backpack.RemoveFromInventory(_decoratedItem);
            player.Backpack.AddToInventory(this);
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
    }
}