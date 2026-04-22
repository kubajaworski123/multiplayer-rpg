using MazeGame.Controller.GameStates;
using MazeGame.Model.Characters;
using MazeGame.Model.Combat;
using MazeGame.Model.Dungeon;
using MazeGame.Model.Players;
using MazeGame.View.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Controller.CombatHandlers
{
    internal class ChooseAttackHandler(Player player, CombatSession session, GameStateManager manager, Map map) : InputHandler
    {
        private Player _player = player;
        private CombatSession _session = session;
        private GameStateManager _manager = manager;
        private Map _map = map;
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (!_session.ActiveCombat)
            {
                if (_session.LeftActor.Health <= 0)
                {
                    MainController.StopRunning();
                }
                else
                {
                    _manager.PopState();
                }
                return;
            }
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                IAttackVisitor visitor = _session.PossibleAttacks[_session.HighlightedAttackOtption];

                _player.CurrentAttackStyle = visitor;
                _session.PlayersTurn();
                if (!_session.ActiveCombat)
                {
                    if (_session.RightActor.Health <= 0)
                    {
                        _session.RightActor.SetBanner(CharacterBanners.DeadBanner);
                        _map.RemoveCombatant(_session.RightActor);
                    }
                    else if (_session.LeftActor.Health <= 0)
                    {
                        _session.LeftActor.SetBanner(CharacterBanners.DeadBanner);
                    }
                }
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
