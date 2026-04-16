namespace Framwork;


public class Endpoint(string path, string method, Func<RequestContext, string> handler)
{
    public readonly string Path = path;
    public readonly string Method = method;

    public readonly Func<RequestContext, string> Handler = handler;

    public bool Matches(RequestContext ctx) =>
        ctx.Path.StartsWith(Path) &&
        ctx.Method.Equals(Method, StringComparison.OrdinalIgnoreCase);
}