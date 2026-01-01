using wu25_auction.Application.DTOs.AuctionItems;

namespace wu25_auction.Application.Services;

public interface IAuctionItemService
{
    Task<IEnumerable<AuctionItemDto>> GetAllAsync();
    Task<AuctionItemDto?> GetByIdAsync(string id);
    Task<AuctionItemDto> CreateAsync(CreateAuctionItemDto item);
    Task<AuctionItemDto> PlaceBidAsync(string id,PlaceBidDto bid);
    Task DeleteAsync(string id);
}