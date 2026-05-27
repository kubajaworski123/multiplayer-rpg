using System.Net.Sockets;
using MazeGame.Model.Game;
using MazeGame.Networking.Dto;
using MazeGame.Networking.Messages;

namespace MazeGame.Networking;

public class GameClient
{
    private readonly object _stateLock = new();
    private TcpClient? _client;
    private NetworkStream? _stream;
    private readonly SemaphoreSlim _sendLock = new(1, 1);
    private CancellationTokenSource? _cts;
    public int LocalPlayerId { get; private set; }
    private GameStateDto? _currentState;
    public event Action? StateUpdated;

    public GameStateDto? CurrentState
    {
        get { lock (_stateLock) return _currentState; }
    }

    private void SetState(GameStateDto? state)
    {
        lock (_stateLock) _currentState = state;
    }

    public async Task<bool> ConnectAsync(string host, int port, string playerName)
    {
        _client = new TcpClient();
        await _client.ConnectAsync(host, port);
        _stream = _client.GetStream();
        await SendMessageAsync(new NetworkMessage
        {
            Type = MessageType.Join,
            PlayerName = playerName
        });
        NetworkMessage? response = null;
        while (true)
        {
            var msg = await MessageSerializer.ReceiveAsync(_stream);
            if (msg == null) return false;
            if (msg.Type == MessageType.Error) return false;
            if (msg.Type == MessageType.JoinAccepted)
            {
                response = msg;
                break;
            }
            if (msg.Type == MessageType.StateUpdate && msg.State != null)
                SetState(msg.State);
        }
        LocalPlayerId = response.PlayerId;
        SetState(response.State);
        _cts = new CancellationTokenSource();
        _ = Task.Run(() => ReceiveLoop(_cts.Token));
        return true;
    }

    private async Task ReceiveLoop(CancellationToken ct)
    {
        try
        {
            while (!ct.IsCancellationRequested && _stream != null)
            {
                var msg = await MessageSerializer.ReceiveAsync(_stream, ct);
                if (msg == null) break;
                if ((msg.Type == MessageType.StateUpdate || msg.Type == MessageType.JoinAccepted) && msg.State != null)
                {
                    SetState(msg.State);
                    StateUpdated?.Invoke();
                }
            }
        }
        catch { }
    }

    public async Task SendActionAsync(PlayerAction action)
    {
        if (_stream == null) return;
        action.PlayerId = LocalPlayerId;
        await SendMessageAsync(new NetworkMessage
        {
            Type = MessageType.Action,
            Action = action
        });
    }

    private async Task SendMessageAsync(NetworkMessage message)
    {
        if (_stream == null) return;
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

    public void Disconnect()
    {
        _cts?.Cancel();
        try
        {
            if (_stream != null)
                SendMessageAsync(new NetworkMessage { Type = MessageType.Disconnect }).Wait(500);
        }
        catch { }
        _client?.Close();
    }
}
