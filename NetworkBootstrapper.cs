using MazeGame.Controller;
using MazeGame.Model.Builders;
using MazeGame.Model.Configuration;
using MazeGame.Model.Dungeon.Themes;
using MazeGame.Model.Game;
using MazeGame.Model.Loggers;
using MazeGame.Model.Observers;
using MazeGame.Networking;
using MazeGame.Networking.Dto;
using MazeGame.View;
using MazeGame.View.Renderers;

namespace MazeGame;

public class NetworkBootstrapper
{
    public void Start(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var (isServer, host, port) = ParseArgs(args);
        if (!isServer.HasValue)
        {
            Console.Write("Uruchomic jako (S)erwer czy (K)lient? ");
            var ch = Console.ReadKey(true).KeyChar;
            isServer = ch is 'S' or 's';
        }

        Config config = ConfigLoader.LoadConfig();
        if (isServer == true)
            RunServer(config, port);
        else
            RunClientAsync(config, host, port).GetAwaiter().GetResult();
    }

    private static (bool? isServer, string host, int port) ParseArgs(string[] args)
    {
        string host = "127.0.0.1";
        int port = 5555;
        if (args.Length == 0) return (null, host, port);
        if (args[0] == "--server")
        {
            if (args.Length > 1 && int.TryParse(args[1], out int p)) port = p;
            return (true, host, port);
        }
        if (args[0] == "--client")
        {
            if (args.Length > 1)
            {
                var parts = args[1].Split(':');
                host = parts[0];
                if (parts.Length > 1) int.TryParse(parts[1], out port);
            }
            return (false, host, port);
        }
        return (null, host, port);
    }

    private void RunServer(Config config, int port)
    {
        var welcome = new WelcomeScreenRenderer();
        welcome.Prepare();
        welcome.Render();

        var theme = ThemeProcessor.GetTheme(config.ThemeName);
        var level = BuilderDirector.ProcessLevel(theme);

        var msgRenderer = new ThemeMessageRendrerer(theme.WelcomeMessage);
        msgRenderer.Prepare();
        msgRenderer.Render();

        var world = new GameWorld(level.Map, level.Controls, level.UsersManual);
        SetupLogging(config, world);

        int hostId = world.AddPlayer(config.PlayerName);
        var server = new GameServer(world, port);
        server.Start();

        Console.Clear();
        Console.WriteLine($"Serwer nasluchuje na porcie {port}. Oczekiwanie na graczy...");
        Thread.Sleep(800);

        var frameRenderer = new NetworkFrameRenderer();
        frameRenderer.Render(server.GetState(), hostId);
        var combatRenderer = new NetworkCombatRenderer();
        var overlay = new NetworkOverlayManager(level.UsersManual);
        int lastGameState = -1;
        while (world.IsRunning)
        {
            server.ProcessTick();
            var state = server.GetState();
            if (IsLocalPlayerEliminated(state, hostId))
                break;
            overlay.SyncLogs(state.AllLogs);
            bool overlayActive = overlay.HasOverlay;

            if (!overlayActive && state.GameStateN != lastGameState)
            {
                var combat = state.Combats.FirstOrDefault(c => c.PlayerId == hostId);
                if (combat != null)
                    combatRenderer.Render(combat);
                else
                    frameRenderer.Render(state, hostId);
                lastGameState = state.GameStateN;
            }

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (overlayActive)
                {
                    overlay.HandleInput(key);
                    if (!world.IsRunning) break;
                    if (!overlay.HasOverlay)
                        lastGameState = -1;
                }
                else if (key.Key == KeyBinds.HelpKey)
                {
                    overlay.PushManual();
                    overlay.Render();
                }
                else if (key.Key == KeyBinds.LogsKey)
                {
                    overlay.PushLogs();
                    overlay.Render();
                }
                else
                {
                    bool inCombat = state.Combats.Any(c => c.PlayerId == hostId);
                    var action = PlayerActionMapper.FromKey(hostId, key, inCombat);
                    if (action?.Type == PlayerActionType.Exit)
                    {
                        server.EnqueueAction(action);
                        server.ProcessTick();
                        break;
                    }
                    if (action != null)
                        server.EnqueueAction(action);
                }
            }
            Thread.Sleep(16);
        }

        server.Stop();
        ShowGameOver();
    }

    private async Task RunClientAsync(Config config, string host, int port)
    {
        Console.Write("Twoja nazwa: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) name = config.PlayerName;

        var client = new GameClient();
        Console.WriteLine($"Laczenie z {host}:{port}...");
        bool ok;
        try
        {
            ok = await client.ConnectAsync(host, port, name);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Blad polaczenia: {ex.Message}");
            Console.WriteLine("Upewnij sie, ze serwer jest uruchomiony.");
            Console.ReadKey(true);
            return;
        }
        if (!ok)
        {
            Console.WriteLine("Nie udalo sie polaczyc.");
            Console.ReadKey(true);
            return;
        }

        var state = client.CurrentState!;
        var frameRenderer = new NetworkFrameRenderer();
        var combatRenderer = new NetworkCombatRenderer();
        var overlay = new NetworkOverlayManager(state.UsersManual);
        int lastGameState = -1;

        while (client.CurrentState?.IsRunning == true)
        {
            state = client.CurrentState!;
            int id = client.LocalPlayerId;
            if (IsLocalPlayerEliminated(state, id))
                break;
            overlay.SyncLogs(state.AllLogs);
            bool overlayActive = overlay.HasOverlay;

            if (!overlayActive && state.GameStateN != lastGameState)
            {
                var combat = state.Combats.FirstOrDefault(c => c.PlayerId == id);
                if (combat != null)
                    combatRenderer.Render(combat);
                else
                    frameRenderer.Render(state, id);
                lastGameState = state.GameStateN;
            }

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (overlayActive)
                {
                    overlay.HandleInput(key);
                    if (!overlay.HasOverlay)
                        lastGameState = -1;
                }
                else if (key.Key == KeyBinds.HelpKey)
                {
                    overlay.PushManual();
                    overlay.Render();
                }
                else if (key.Key == KeyBinds.LogsKey)
                {
                    overlay.PushLogs();
                    overlay.Render();
                }
                else
                {
                    bool inCombat = state.Combats.Any(c => c.PlayerId == id);
                    var action = PlayerActionMapper.FromKey(id, key, inCombat);
                    if (action != null)
                        await client.SendActionAsync(action);
                }
            }
            Thread.Sleep(16);
        }

        client.Disconnect();
        ShowGameOver();
    }

    private static bool IsLocalPlayerEliminated(GameStateDto state, int playerId)
    {
        var player = state.Players.FirstOrDefault(p => p.Id == playerId);
        return player == null || !player.IsConnected || player.Health <= 0;
    }

    private static void ShowGameOver()
    {
        var over = new GameOverRenderer();
        over.Prepare();
        over.Render();
    }

    private static void SetupLogging(Config config, GameWorld world)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string logFileName = $"{config.PlayerName}_{timestamp}.txt";
        string logPath = Path.Combine(config.LogDirectory, logFileName);
        if (!Directory.Exists(config.LogDirectory))
            Directory.CreateDirectory(config.LogDirectory);

        LogBus.Logs.Subscribe(new FileLoggerObserver(logPath));
        LogBus.Logs.Subscribe(new RecentLogBridge(world));
    }
}

