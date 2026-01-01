using Microsoft.EntityFrameworkCore;
using wu25_auction.Domain.Entities;

namespace wu25_auction.DataAccess.Repositories;

public class AuctionItemRepository : IAuctionItemRepository
{
    private readonly AuctionDbContext _context;
    
    public AuctionItemRepository(AuctionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuctionItem>> GetAllAsync()
    {
        return await _context.AuctionsItems.ToListAsync();
    }

    public async Task<AuctionItem?> GetByIdAsync(string id)
    {
        return await _context.AuctionsItems
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<AuctionItem> AddAsync(AuctionItem auctionItem)
    {
        _context.AuctionsItems.Add(auctionItem);
        await _context.SaveChangesAsync();
        return auctionItem;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}