using wu25_auction.Domain.ValueObjects;

namespace wu25_auction.Web.Models;

public class UserViewModel
{
    public string Id { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
    public IList<string> AllRoles { get; set; } = new List<string>();
    public Address? Address { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ImageUrl { get; set; }
}