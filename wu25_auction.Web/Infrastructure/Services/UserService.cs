using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using wu25_auction.Web.Data.Entities;
using wu25_auction.Web.Models;

namespace wu25_auction.Web.Infrastructure.Services;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        var users = await userManager.Users.ToListAsync();
        
        return users.Select(user => new UserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        });
    }
}