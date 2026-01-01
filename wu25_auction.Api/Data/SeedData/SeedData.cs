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
            new AuctionItem(
                "Vintage klocka",
                "En klassisk mekanisk klocka från 60-talet",
                1000,
                DateTime.UtcNow.AddDays(7)
            ),
            new AuctionItem(
                "Gaming laptop",
                "Kraftfull laptop för spel och programmering",
                5000,
                DateTime.UtcNow.AddDays(5)
            ),
            new AuctionItem(
                "Elgitarr",
                "Fender Stratocaster i mycket fint skick",
                7500,
                DateTime.UtcNow.AddDays(10)
            )
        };
        
        context.AuctionsItems.AddRange(items);
        await context.SaveChangesAsync();
    }
}