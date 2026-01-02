using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wu25_auction.Web.Models;
using wu25_auction.Web.Services;

namespace wu25_auction.Web.Pages.Auctions;

public class AuctionsIndexModel(
    AuctionItemHttpClientService httpClientService, IEnumerable<AuctionItemViewModel> items) 
    : PageModel
{
    public IEnumerable<AuctionItemViewModel> Items { get; private set; } = items;

    public async Task<IActionResult> OnGetAsync()
    {
        Items = await httpClientService.GetAllAsync();
        return Page();
    }
}