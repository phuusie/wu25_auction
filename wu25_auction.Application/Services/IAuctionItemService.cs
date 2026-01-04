using wu25_auction.Application.DTOs.AuctionItems;
using wu25_auction.Domain.Entities;

namespace wu25_auction.Application.Services;

public interface IAuctionItemService
{
    Task<string> CreateAsync(AuctionItemDto dto, string userId);
    Task<IEnumerable<AuctionItem>> GetAllAsync();
    Task<AuctionItem?> GetByIdAsync(string id);
    Task<IEnumerable<AuctionItem>> GetAllByUserIdAsync(string userId);
    Task MarkAsSoldAsync(string id, decimal price, bool isManager);
}