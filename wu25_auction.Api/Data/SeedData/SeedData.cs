using wu25_auction.DataAccess;
using wu25_auction.Domain.Entities;

namespace wu25_auction.Api.Data.SeedData;

public static class SeedData
{
    public static async Task InitializeAsync(AuctionDbContext context)
    {
        if (context.AuctionsItems.Any())
            return;

        var items = new List<AuctionItem>
        {
            new(
                category: "Dekoration",
                decade: 1960,
                description: "Vintage klocka från 60-talet",
                purchasePrice: 1000,
                startPrice: 1500,
                createdBy: "seed-user"
            ),

            new(
                category: "Elektronik",
                decade: 2010,
                description: "Gaming laptop i gott skick",
                purchasePrice: 5000,
                startPrice: 6500,
                createdBy: "seed-user"
            ),

            new(
                category: "Musik",
                decade: 1970,
                description: "Fender Stratocaster elgitarr",
                purchasePrice: 7500,
                startPrice: 9500,
                createdBy: "seed-user"
            )
        };
        
        context.AuctionsItems.AddRange(items);
        await context.SaveChangesAsync();
    }
}