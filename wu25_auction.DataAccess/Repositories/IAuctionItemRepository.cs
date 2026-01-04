using wu25_auction.Domain.Entities;

namespace wu25_auction.DataAccess.Repositories;

public interface IAuctionItemRepository
{
    Task AddAsync(AuctionItem item);
    Task<IEnumerable<AuctionItem>> GetAllAsync();
    Task<IEnumerable<AuctionItem>> GetAllByUserIdAsync(string userId);
    Task<AuctionItem?> GetByIdAsync(string id);
    Task SaveChangesAsync();
}