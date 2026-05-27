using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using MazeGame.Model.Game;
using MazeGame.Networking.Dto;
using MazeGame.Networking.Messages;

namespace MazeGame.Networking;

public class GameServer
{
    private readonly GameWorld _world;
    private readonly int _port;
    private readonly ConcurrentQueue<PlayerAction> _actionQueue = new();
    private readonly List<ClientSession> _clients = new();
    private readonly object _clientLock = new();
    private TcpListener? _listener;
    private CancellationTokenSource? _cts;
    public event Action<GameStateDto>? StateChanged;

    public GameServer(GameWorld world, int port)
    {
        _world = world;
        _port = port;
    }

    public void EnqueueAction(PlayerAction action) => _actionQueue.Enqueue(action);

    public void Start()
    {
        _cts = new CancellationTokenSource();
        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();
        Task.Run(() => AcceptLoop(_cts.Token));
    }

    public void Stop()
    {
        _cts?.Cancel();
        _listener?.Stop();
        lock (_clientLock)
        {
            foreach (var c in _clients) c.Close();
            _clients.Clear();
        }
    }

    public void ProcessTick()
    {
        bool changed = false;
        while (_actionQueue.TryDequeue(out var action))
        {
            _world.ProcessAction(action);
            changed = true;
        }
        if (changed) 
            BroadcastState();
    }

    private async Task AcceptLoop(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested && _listener != null)
        {
            try
            {
                var client = await _listener.AcceptTcpClientAsync(ct);
                if (_world.Players.Count >= 9)
                {
                    client.Close();
                    continue;
                }
                var session = new ClientSession(client, this);
                lock (_clientLock) _clients.Add(session);
                _ = Task.Run(() => session.RunAsync(ct), ct);
            }
            catch (OperationCanceledException) { break; }
            catch { }
        }
    }

    internal int RegisterPlayer(string name)
    {
        return _world.AddPlayer(name);
    }

    internal void RemoveSession(ClientSession session, int playerId)
    {
        lock (_clientLock) _clients.Remove(session);
        if (playerId > 0) _world.RemovePlayer(playerId);
        BroadcastState();
    }

    internal void OnClientAction(PlayerAction action) => _actionQueue.Enqueue(action);

    internal void BroadcastState()
    {
        var dto = GameStateMapper.ToDto(_world);
        StateChanged?.Invoke(dto);
        var msg = new NetworkMessage {
            Type = MessageType.StateUpdate,
            State = dto 
        };
        List<ClientSession> clients;
        lock (_clientLock)
            clients = _clients.ToList();
        foreach (var c in clients)
            c.SendAsync(msg).GetAwaiter().GetResult();
    }

    internal async Task SendJoinAccepted(ClientSession session, int playerId)
    {
        var dto = GameStateMapper.ToDto(_world);
        await session.SendAsync(new NetworkMessage
        {
            Type = MessageType.JoinAccepted,
            PlayerId = playerId,
            State = dto
        });
    }

    public GameStateDto GetState() => GameStateMapper.ToDto(_world);
}

internal class ClientSession
{
    private readonly TcpClient _client;
    private readonly GameServer _server;
    private readonly NetworkStream _stream;
    private readonly SemaphoreSlim _sendLock = new(1, 1);
    public int PlayerId { get; private set; }

    public ClientSession(TcpClient client, GameServer server)
    {
        _client = client;
        _server = server;
        _stream = client.GetStream();
    }

    public async Task RunAsync(CancellationToken ct)
    {
        try
        {
            var first = await MessageSerializer.ReceiveAsync(_stream, ct);
            if (first?.Type != MessageType.Join || string.IsNullOrWhiteSpace(first.PlayerName))
            {
                Close();
                return;
            }
            PlayerId = _server.RegisterPlayer(first.PlayerName!);
            if (PlayerId < 0)
            {
                await SendAsync(new NetworkMessage { Type = MessageType.Error, Error = "Server full" });
                Close();
                return;
            }
            await _server.SendJoinAccepted(this, PlayerId);
            _server.BroadcastState();
            while (!ct.IsCancellationRequested && _client.Connected)
            {
                var msg = await MessageSerializer.ReceiveAsync(_stream, ct);
                if (msg == null) break;
                if (msg.Type == MessageType.Action && msg.Action != null)
                {
                    msg.Action.PlayerId = PlayerId;
                    _server.OnClientAction(msg.Action);
                }
                else if (msg.Type == MessageType.Disconnect) break;
            }
        }
        catch { }
        finally
        {
            _server.RemoveSession(this, PlayerId);
            Close();
        }
    }

    public async Task SendAsync(NetworkMessage message)
    {
        await _sendLock.WaitAsync();
        try
        {
            await MessageSerializer.SendAsync(_stream, message);
        }
        finally
        {
            _sendLock.Release();
        }
    }

    public void Close()
    {
        try { 
            _client.Close();
        }
        catch { }
    }
}
