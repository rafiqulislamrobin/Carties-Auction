
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data
{
    public class DbInitialSeeder
    {
        public static void DbInitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            
            SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());
        }

        private static void SeedData(AuctionDbContext? auctionDbContext)
        {
            auctionDbContext.Database.Migrate();
        }
    }
}
