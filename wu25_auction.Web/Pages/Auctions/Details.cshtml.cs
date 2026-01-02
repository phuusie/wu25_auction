using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wu25_auction.Web.Infrastructure.Services;
using wu25_auction.Web.Models;

namespace wu25_auction.Web.Pages.Auctions;

public class AuctionsDetailsModel(
    IAuctionItemHttpClientService httpClientService, IEnumerable<AuctionItemViewModel> items) 
    : PageModel
{
    public AuctionItemViewModel? Item { get; private set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        Item = await httpClientService.GetByIdAsync(id);
        return Page();
    }
}