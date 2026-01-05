using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using wu25_auction.Web.Data.Entities;
using wu25_auction.Web.Models;

namespace wu25_auction.Web.Infrastructure.Services;

public class UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IUserService
{
    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        var users = userManager.Users.ToList();
        var userList = new List<UserViewModel>();

        foreach (var user in users)
        {
            userList.Add(new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Street = user.Address?.Street,
                City = user.Address?.City,
                Country = user.Address?.Country,
                PostalCode = user.Address?.PostalCode,
                PhoneNumber = user.PhoneNumber,
                Roles = await userManager.GetRolesAsync(user),
                AllRoles = roleManager.Roles.Select(r => r.Name!).ToList()
            });
        }
        
        return userList;
    }

    public async Task<UserViewModel> GetByIdAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        var userViewModel = new UserViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Street = user.Address?.Street,
            City = user.Address?.City,
            Country = user.Address?.Country,
            PostalCode = user.Address?.PostalCode,
            PhoneNumber = user.PhoneNumber,
            Roles = await userManager.GetRolesAsync(user),
            AllRoles = roleManager.Roles.Select(r => r.Name!).ToList()
        };
        
        return userViewModel;
    }

    public async Task UpdateUserASync(UserViewModel user)
    {
        var selectedUser = await userManager.FindByIdAsync(user.Id);

        selectedUser.Update(
            user.FirstName,
            user.LastName,
            user.Address);
        
        var result = await userManager.UpdateAsync(selectedUser);
        
        if (!result.Succeeded)
            throw new Exception(string.Join(", ",
                result.Errors.Select(e => e.Description)));
    }

    public async Task UpdateRolesAsync(string adminId, string userId, List<string> roles)
    {
        var user = await userManager.FindByIdAsync(userId);

        var currentRoles = await userManager.GetRolesAsync(user);
        
        if (adminId == userId && !roles.Contains("Admin"))
            throw new InvalidOperationException(
                "Du kan inte ta bort din egna Admin-roll");
        
        await userManager.RemoveFromRolesAsync(user, currentRoles);
        await userManager.AddToRolesAsync(user, roles ?? new());
    }

    public async Task DeleteUserAsync(string adminId, string userId)
    {
        if (adminId == userId)
            throw new InvalidOperationException("Du kan inte ta bort dig själv");
        
        var user = await userManager.FindByIdAsync(userId);
        
        await userManager.DeleteAsync(user);
    }
}