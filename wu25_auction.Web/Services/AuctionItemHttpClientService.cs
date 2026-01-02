using wu25_auction.Web.Models;

namespace wu25_auction.Web.Services;

public class AuctionItemHttpClientService(HttpClient httpClient) : IAuctionItemHttpClientService
{
    
    public async Task<IEnumerable<AuctionItemViewModel>> GetAllAsync()
    {
        var items = await httpClient.GetFromJsonAsync<IEnumerable<AuctionItemViewModel>>("api/auctions");
        return items ?? [];
    }

    public async Task<AuctionItemViewModel?> GetByIdAsync(string id)
    {
        var item = await httpClient.GetFromJsonAsync<AuctionItemViewModel>($"api/auctions/{id}");
        
        return item;
    }

    public async Task CreateAsync(AuctionItemViewModel vm)
    {
        await httpClient.PostAsJsonAsync("api/auctions", vm);
    }
}