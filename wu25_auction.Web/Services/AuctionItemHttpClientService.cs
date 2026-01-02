using wu25_auction.Web.Models;

namespace wu25_auction.Web.Services;

public class AuctionItemHttpClientService(HttpClient httpClient) : IAuctionItemHttpClientService
{
    
    public async Task<IEnumerable<AuctionItemViewModel>> GetAllAsync()
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<AuctionItemViewModel>>(
                "api/auctions") ?? [];
    }

    public async Task<AuctionItemViewModel?> GetByIdAsync(string id)
    {
        return await httpClient.GetFromJsonAsync<AuctionItemViewModel>(
            $"api/auctions/{id}");
    }

    public async Task CreateAsync(AuctionItemViewModel vm)
    {
        await httpClient.PostAsJsonAsync("api/auctions", vm);
    }
}