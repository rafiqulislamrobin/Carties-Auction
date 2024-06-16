using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using System.Text.Json;

namespace SearchService;

public class DbInitializer
{


    public static async Task InitDb(WebApplication app)
    {
        var x = app.Configuration.GetConnectionString("MongoDbConnection");
        // Initialize the database connection
        await DB.InitAsync("SearchDb", MongoClientSettings
                        .FromConnectionString(x));

        // Ensure that the indexes are created
        await DB.Index<Item>()
                .Key(x => x.Make, KeyType.Text)
                .Key(x => x.Model, KeyType.Text)
                .Key(x => x.Color, KeyType.Text)
                .CreateAsync();

        // Check the count of items in the database
        var count = await DB.CountAsync<Item>();

        using var scope = app.Services.CreateScope();

        var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();

        var items = await httpClient.GetItemsForSearchDb();

        Console.WriteLine(items.Count + " returned from the auction service");

        if (items.Count > 0) await DB.SaveAsync(items);
    }
}