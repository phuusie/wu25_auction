using Microsoft.AspNetCore.Mvc.RazorPages;
using wu25_auction.Web.Infrastructure.Services;

namespace wu25_auction.Web.Pages.User;

public class UserInventoryModel(IAuctionItemHttpClientService httpClientService) : PageModel
{
    
}