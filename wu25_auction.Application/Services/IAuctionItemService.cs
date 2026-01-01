using wu25_auction.Application.DTOs;

namespace wu25_auction.Application.Services;

public interface IAuctionItemService
{
    Task<IEnumerable<AuctionItemDto>> GetAllAsync();
    Task<AuctionItemDto?> GetByIdAsync(int id);
    Task<AuctionItemDto> CreateAsync(CreateAuctionItemDto auctionItem);
}