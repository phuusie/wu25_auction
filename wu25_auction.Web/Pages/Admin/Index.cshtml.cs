using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wu25_auction.Web.Infrastructure.Services;
using wu25_auction.Web.Models;

namespace wu25_auction.Web.Pages.Admin;

public class AdminModel(IUserService userService) 
    : PageModel
{
    public IEnumerable<UserViewModel> Users { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Users = await userService.GetAllAsync();
        return Page();
    }
}


