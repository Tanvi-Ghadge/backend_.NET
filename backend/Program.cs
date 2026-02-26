using backend.endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapEndpoints();
app.MapGet("/", () => "Hello World!");

app.Run();
