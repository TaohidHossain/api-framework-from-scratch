namespace Framwork;

public class Router
{
    private readonly List<Endpoint> _endpoints = [];

    public void MapGet(string path, Func<RequestContext, string> handler)
    {
        _endpoints.Add(new(path, "GET", handler));
    }

    public void MapPost(string path, Func<RequestContext, string> handler)
    {
        _endpoints.Add(new(path, "POST", handler));
    }

    public string Resolve(RequestContext ctx)
    {
        var endpoint = _endpoints.FirstOrDefault(ep => ep.Matches(ctx));
        
        Console.WriteLine($"Received {ctx.Method} request for {ctx.Path}. Resolved to: {(endpoint != null ? endpoint.Path : "No match")}");
        return endpoint != null
            ? endpoint.Handler(ctx)
            : "404 Not Found";
    }
}