using MazeGame.Controller;
using MazeGame.Controller.AllLogsHandlers;
using MazeGame.Controller.GameStates;
using MazeGame.Controller.MainMapHandlers;
using MazeGame.Controller.UsersManualHandlers;
using MazeGame.Model.Builders;
using MazeGame.Model.Characters;
using MazeGame.Model.Configuration;
using MazeGame.Model.Dungeon.Themes;
using MazeGame.Model.Observers;
using MazeGame.Model.Players;
using MazeGame.View;
using MazeGame.View.Renderers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MazeGame
{
    public class GameBootstrapper
    {
        public void Start()
        {
            SetupConsole();
            
            Config config = ConfigLoader.LoadConfig();

            Player player = SetupPlayer();
            player.SetName(config.PlayerName);

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string logFileName = $"{config.PlayerName}_{timestamp}.txt";
            string logPath = Path.Combine(config.LogDirectory, logFileName);
            if (!Directory.Exists(config.LogDirectory))
            {
                Directory.CreateDirectory(config.LogDirectory);
            }

            ShowWelcomeScreen();
            var theme = ThemeProcessor.GetTheme(config.ThemeName);

            LevelData level = BuilderDirector.ProcessLevel(theme);

            ShowIntro(level);
            ShowThemeMsg(theme.WelcomeMessage);
            GameStateManager stateManager = InitializeGameMachine(player, level, logPath, theme);

            var controller = new MainController(stateManager);
            controller.Run();
        }

        private void ShowThemeMsg(string msg)
        {
            var msgRenderer = new ThemeMessageRendrerer(msg);
            msgRenderer.Prepare();
            msgRenderer.Render();
        }

        private void SetupConsole()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        private Player SetupPlayer()
        {
            Player player = new Player();
            player.SetType(CharacterType.Player);
            return player;
        }

        private void ShowWelcomeScreen()
        {
            var mainScreenRenderer = new WelcomeScreenRenderer();
            mainScreenRenderer.Prepare();
            mainScreenRenderer.Render();
        }

        private void ShowIntro(LevelData level)
        {
            var subRenderer = new SWIntroRenderer(level.Subtitles);
            var ct = new CancellationTokenSource();
            Task.Run(() => ViewUtils.PlayImperialMarchLooped(ct.Token));
            subRenderer.Prepare();
            subRenderer.Render();
            ct.Cancel();
        }

        private GameStateManager InitializeGameMachine(Player player, LevelData level, string logPath, IDungeonThemeFactory theme)
        {
            var stateManager = new GameStateManager();

            var fileLogger = new FileLoggerObserver(logPath);
            var mapViewLogger = new MapViewLogger();
            var allLogger = new AllLogsObserver();
                
            LogBus.Logs.Subscribe(fileLogger);
            LogBus.Logs.Subscribe(mapViewLogger);
            LogBus.Logs.Subscribe(allLogger);

            var renderer = new FrameRenderer(player, level.Map, level.Controls, mapViewLogger);

            var umRenderer = new UserManualRenderer(level.UsersManual);
            var gameOverRenderer = new GameOverRenderer();
            umRenderer.Prepare();

            var exitUM = new ExitUsersManualHandler(stateManager);
            var manualState = new UserManualState(umRenderer, exitUM);
            var gameOverState = new GameOverState(gameOverRenderer, null);

            var exitAllLogs = new ExitAllLogsHandler(stateManager);
            AllLogsRenderer allLogsRenderer = new AllLogsRenderer(allLogger.AllLogs);
            var allLoggerState = new AllLogsState(allLogsRenderer,exitAllLogs);

            var scrollUp = new ScrollUpHandler(umRenderer);
            var scrollDown = new ScrollDownHandler(umRenderer);
            var exitFromManual = new ExitHandler(stateManager, gameOverState);

            exitUM.SetNextHandler(exitFromManual);
            exitFromManual.SetNextHandler(scrollUp);
            scrollUp.SetNextHandler(scrollDown);

            var scrollLogs = new ScrollAllLogsHandler(allLogsRenderer);
            exitAllLogs.SetNextHandler(scrollLogs);

            var corBuilder = new CORMapBuilder(player, level.Map, renderer, stateManager, manualState, gameOverState, allLoggerState);
            theme.ApplyToBuilder(corBuilder);
            var mapInputChain = corBuilder.GetCOR();

            var mapState = new MapState(renderer, mapInputChain);
            stateManager.PushState(mapState);

            return stateManager;
        }
    }
}