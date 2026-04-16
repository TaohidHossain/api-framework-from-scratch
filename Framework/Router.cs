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

        return endpoint != null
            ? endpoint.Handler(ctx)
            : "404 Not Found";
    }
}