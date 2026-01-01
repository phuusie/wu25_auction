using wu25_auction.Application.DTOs.AuctionItems;

namespace wu25_auction.Web.Services;

public class AuctionItemApiClient
{
    private readonly HttpClient _httpClient;
    
    public AuctionItemApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<AuctionItemDto>> GetAllAuctionItemsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<AuctionItemDto>>(
                "api/auctions") ?? [];
    }

    public async Task<AuctionItemDto?> GetAuctionItemById(string id)
    {
        return await _httpClient.GetFromJsonAsync<AuctionItemDto>(
            $"api/auctions/{id}");
    }

    public async Task CreateAsync(AuctionItemDto dto)
    {
        await _httpClient.PostAsJsonAsync("api/auctions", dto);
    }
}