using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wu25_auction.Web.Models;
using wu25_auction.Web.Services;

namespace wu25_auction.Web.Pages.Auctions;

public class AuctionsDetailsModel(
    AuctionItemHttpClientService httpClientService, IEnumerable<AuctionItemViewModel> items) 
    : PageModel
{
    public AuctionItemViewModel? Item { get; private set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        Item = await httpClientService.GetByIdAsync(id);
        return Page();
    }
}