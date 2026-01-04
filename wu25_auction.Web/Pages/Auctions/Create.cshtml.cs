using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wu25_auction.Web.Data.Entities;
using wu25_auction.Web.Infrastructure.Services;
using wu25_auction.Web.Models;

namespace wu25_auction.Web.Pages.Auctions
{
    public class CreateModel(
        IAuctionItemHttpClientService httpClientService, UserManager<ApplicationUser> userManager) : PageModel
    {
        [BindProperty]
        public AuctionItemViewModel Item { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            await httpClientService.CreateAsync(Item, user.Id);
            return RedirectToPage("./Index");
        }
    }
}