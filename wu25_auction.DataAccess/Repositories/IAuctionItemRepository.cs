using wu25_auction.Domain.Entities;

namespace wu25_auction.DataAccess.Repositories;

public interface IAuctionItemRepository
{
    Task<IEnumerable<AuctionItem>> GetAllAsync();
    Task<AuctionItem?> GetByIdAsync(int id);
    Task<AuctionItem> AddAsync(AuctionItem auctionItem);
}