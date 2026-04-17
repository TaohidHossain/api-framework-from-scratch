using Framwork;

var builder = WebApplicationAFactory.CreateBuilder();
var app = builder.Build();

app.MapGet("/hello", context =>
{
    return "Hello, World!";
});

app.MapGet("/users", context =>
{
    return "List of users";
});

app.MapGet("/", context =>
{
    return "Welcome to the API!";
});

await app.RunAsync(3000);
