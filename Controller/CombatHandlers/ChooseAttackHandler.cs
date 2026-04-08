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
    internal class ChooseAttackHandler(Player player, CombatRenderer renderer ,CombatSession session, GameStateManager manager, Map map) : InputHandler
    {
        private Player _player = player;
        private CombatSession _session = session;
        private CombatRenderer _renderer = renderer;
        private GameStateManager _manager = manager;
        private Map _map = map;
        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                IAttackVisitor visitor = _session.PossibleAttacks[_session.HighlightedAttackOtption];

                _player.CurrentAttackStyle = visitor;
                _session.PlayersTurn();
                _renderer.Prepare();
                _renderer.Render();
                if (!_session.ActiveCombat)
                {
                    if (_session.RightActor.Health <= 0)
                    {

                        _session.RightActor.SetBanner(CharacterBanners.DeadBanner);
                        _renderer.Prepare();
                        _renderer.Render();
                        _map.RemoveCombatant(_session.RightActor);
                        Console.ReadKey();
                    }
                    else if (_session.LeftActor.Health <= 0)
                    {
                        _session.LeftActor.SetBanner(CharacterBanners.DeadBanner);
                        _renderer.Prepare();
                        _renderer.Render();
                        Console.ReadKey();
                        MainController.StopRunning();
                    }
                    _manager.PopState();
                }
            }
            else
            {
                base.Handle(keyInfo);
            }
        }
    }
}
