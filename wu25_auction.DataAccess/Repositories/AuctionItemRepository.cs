using Microsoft.EntityFrameworkCore;
using wu25_auction.Domain.Entities;

namespace wu25_auction.DataAccess.Repositories;

public class AuctionItemRepository(AuctionDbContext context) : IAuctionItemRepository
{
    public async Task AddAsync(AuctionItem item)
    {
        context.AuctionsItems.Add(item);
        await context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<AuctionItem>> GetAllAsync()
    {
        return await context.AuctionsItems.ToListAsync();
    }

    public async Task<AuctionItem?> GetByIdAsync(string id)
    {
        return await context.AuctionsItems
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}