using MazeGame.Model.Observers;

namespace MazeGame.Model.Characters
{
    public class DroidMob : Mob
    {
        public DroidMob(int x = 1, int y = 1, IMobPublisher? mobGroup = null, string name = "Droid", char symbol = 'D',
            int health = 80, CharacterType characterType = CharacterType.Droid, int attack = 4, int defence = 2)
            : base(name, symbol, health, x, y, characterType, attack, defence, mobGroup)
        {
        }

        public override void OnFriendsDeath()
        {
        }
    }
}
