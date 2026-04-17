namespace Framwork;

class WebApplication
{
    private readonly Router _router = new();

    public WebApplication MapGet(string pattern, Func<RequestContext, string> handler)
    {
        _router.MapGet(pattern, handler);
        return this;
    }

    public WebApplication MapPost(string pattern, Func<RequestContext, string> handler)
    {
        _router.MapPost(pattern, handler);
        return this;
    }

    public async Task RunAsync(int port = 5000)
    {
        var server = new TcpServer(port, _router);
        await server.StartAsync();
    } 

}

class WebApplicationBuilder
{
    public WebApplication Build()
    {
        return new WebApplication();
    }
}
class WebApplicationAFactory
{
    public static WebApplicationBuilder CreateBuilder()
    {
        return new WebApplicationBuilder();
    }
}