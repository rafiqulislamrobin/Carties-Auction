using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using System.Text.Json;

namespace SearchService;

public class DbInitializer
{
    //public static async Task InitDb(WebApplication app)
    //{


    //    //await DB.InitAsync("SearchDb", MongoClientSettings
    //    //    .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));
    //    var connectionString = "mongodb://root:mongo@localhost:27017/admin?authMechanism=SCRAM-SHA-256";
    //    var settings = MongoClientSettings.FromConnectionString(connectionString);
    //    var client = new MongoClient(settings);
    //    var database = client.GetDatabase("SearchDb");

    //    await DB.Index<Item>()
    //        .Key(x => x.Make, KeyType.Text)
    //        .Key(x => x.Model, KeyType.Text)
    //        .Key(x => x.Color, KeyType.Text)
    //        .CreateAsync();

    //    var count = await DB.CountAsync<Item>();

    //    using var scope = app.Services.CreateScope();

    //    //var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();

    //    //var items = await httpClient.GetItemsForSearchDb();

    //    //Console.WriteLine(items.Count + " returned from the auction service");

    //    var itemsData = await File.ReadAllTextAsync("Data/Auction.json");
    //    var items = JsonSerializer.Deserialize<List<Item>> (itemsData);
    //    if (items.Count > 0) await DB.SaveAsync(items);
    //}

    public static async Task InitDb(WebApplication app)
    {
        var x = app.Configuration.GetConnectionString("MongoDbConnection");
        // Initialize the database connection
        await DB.InitAsync("SearchDb", MongoClientSettings
            .FromConnectionString(x));

        try
        {
            // Ensure that the indexes are created
            await DB.Index<Item>()
                .Key(x => x.Make, KeyType.Text)
                .Key(x => x.Model, KeyType.Text)
                .Key(x => x.Color, KeyType.Text)
                .CreateAsync();

            // Check the count of items in the database
            var count = await DB.CountAsync<Item>();

            // Create a scope to resolve scoped services
            using var scope = app.Services.CreateScope();

            // Read items data from JSON file
            var itemsData = await File.ReadAllTextAsync("Data/Auction.json");
            var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = JsonSerializer.Deserialize<List<Item>>(itemsData, opt);

            // Save items to the database if there are any
            if (items.Count > 0)
            {
                await DB.SaveAsync(items);
            }
        }
        catch (Exception e)
        {

            throw e;
        }
    }
}