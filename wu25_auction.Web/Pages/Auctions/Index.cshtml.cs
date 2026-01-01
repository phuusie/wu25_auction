using Microsoft.AspNetCore.Mvc.RazorPages;
using wu25_auction.Application.DTOs.AuctionItems;
using wu25_auction.Web.Services;

namespace wu25_auction.Web.Pages.Auctions;

public class AuctionsIndexModel : PageModel
{
    private readonly AuctionItemApiClient _apiClient;
    
    public IEnumerable<AuctionItemDto> Items { get; private set; }

    public AuctionsIndexModel(AuctionItemApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task OnGetAsync()
    {
        Items = await _apiClient.GetAllAuctionItemsAsync();
    }
}