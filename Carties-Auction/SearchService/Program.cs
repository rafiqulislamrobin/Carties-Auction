using MongoDB.Driver;
using MongoDB.Entities;
using SearchService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
try
{
    await DbInitializer.InitDb(app);
}
catch (TypeInitializationException ex)
{
    Console.WriteLine($"Type Initialization Exception: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
    }
}
app.Run();
