using System.Text.Json;
using wu25_auction.DataAccess;
using wu25_auction.Domain.Entities;

namespace wu25_auction.Api.Data.SeedData;

public static class SeedData
{
    public static async Task InitializeAsync(AuctionDbContext context)
    {
        if (context.AuctionsItems.Any())
            return;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var seedDataJson = File.ReadAllText("Data/SeedData/JSON/auctionItems.json");
        
        var seedData = JsonSerializer.Deserialize<List<AuctionItem>>(seedDataJson, options);

        if (seedData != null)
        {
            var auctionItems = seedData
                .Select(a => new AuctionItem(
                    a.Category, a.Decade, a.Description, a.PurchasePrice, a.StartPrice, a.CreatedBy))
                .ToList();
            
            context.AuctionsItems.AddRange(auctionItems);
        }
        
        await context.SaveChangesAsync();
    }
}