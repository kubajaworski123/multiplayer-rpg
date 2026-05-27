using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using MazeGame.Networking.Messages;

namespace MazeGame.Networking;

public static class MessageSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public static async Task SendAsync(NetworkStream stream, NetworkMessage message, CancellationToken ct = default)
    {
        string json = JsonSerializer.Serialize(message, Options);
        byte[] payload = Encoding.UTF8.GetBytes(json);
        byte[] length = BitConverter.GetBytes(payload.Length);
        await stream.WriteAsync(length, ct);
        await stream.WriteAsync(payload, ct);
    }

    public static async Task<NetworkMessage?> ReceiveAsync(NetworkStream stream, CancellationToken ct = default)
    {
        byte[] lengthBuf = new byte[4];
        int read = await ReadExactAsync(stream, lengthBuf, ct);
        if (read == 0)
            return null;
        int length = BitConverter.ToInt32(lengthBuf, 0);
        if (length <= 0)
            return null;
        byte[] payload = new byte[length];
        await ReadExactAsync(stream, payload, ct);
        string json = Encoding.UTF8.GetString(payload);
        return JsonSerializer.Deserialize<NetworkMessage>(json, Options);
    }

    private static async Task<int> ReadExactAsync(NetworkStream stream, byte[] buffer, CancellationToken ct)
    {
        int offset = 0;
        while (offset < buffer.Length)
        {
            int n = await stream.ReadAsync(buffer.AsMemory(offset, buffer.Length - offset), ct);
            if (n == 0) 
                return offset == 0 ? 0 : throw new IOException("Connection closed");
            offset += n;
        }
        return offset;
    }
}
