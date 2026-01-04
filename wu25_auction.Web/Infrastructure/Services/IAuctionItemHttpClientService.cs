using wu25_auction.Web.Models;

namespace wu25_auction.Web.Infrastructure.Services;

public interface IAuctionItemHttpClientService
{
    Task<IEnumerable<AuctionItemViewModel>> GetAllAsync();
    Task<AuctionItemViewModel?> GetByIdAsync(string id);
    Task<IEnumerable<AuctionItemViewModel>> GetAllByUserIdAsync(string userId);
    Task CreateAsync(AuctionItemViewModel vm, string userId);
}