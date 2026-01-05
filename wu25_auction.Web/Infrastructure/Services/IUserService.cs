using wu25_auction.Web.Data.Entities;
using wu25_auction.Web.Models;

namespace wu25_auction.Web.Infrastructure.Services;

public interface IUserService
{
    Task<IEnumerable<UserViewModel>> GetAllAsync();
}