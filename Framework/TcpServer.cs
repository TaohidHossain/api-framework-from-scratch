using System.Net;
using System.Net.Sockets;
using  System.Text;

namespace Framwork;

public class RequestContext
{
    public string Method {get; set;} = string.Empty;
    public string Path {get; set;} = string.Empty;

    public RequestContext(string method, string path)
    {
        Method = method;
        Path = path;
    }
}

public class TcpServer
{
    private readonly int _port;
    private readonly Router _router;
    public TcpServer(int port, Router router)
    {
        _port = port;
        _router = router;
    }

    public async Task StartAsync()
    {
        var listener = new TcpListener(IPAddress.Loopback, _port);
        listener.Start();
        Console.WriteLine($"Server started on port: {_port}");

        while(true)
        {
            var client = await listener.AcceptTcpClientAsync();
            _ = Task.Run(() => HandleClient(client));
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        using var stream = client.GetStream();
        var buffer = new byte[2048];

        var byteCount = await stream.ReadAsync(buffer);
        var requestText = Encoding.UTF8.GetString(buffer, 0, byteCount);
        var lines = requestText.Split("\r\n");
        
        var requestLine = lines[0].Split(" ");
        RequestContext context = new(requestLine[0], requestLine[1]);

        string responseText = _router.Resolve(context);
        var responseByte = Encoding.UTF8.GetBytes(
            "HTTP/1.1 200 OK\r\nContent-Length: " +
            responseText.Length +
            "\r\n\r\n" +
            responseText
        );
        await stream.WriteAsync(responseByte);
        client.Close();
    }
}