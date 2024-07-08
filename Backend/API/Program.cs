using API.Helpers.Program;

var app = WebApplication
    .CreateBuilder(args)
    .AddPlugins()
    .EnablePlugins();

app.Run();