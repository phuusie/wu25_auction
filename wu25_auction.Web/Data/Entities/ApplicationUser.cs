using Microsoft.AspNetCore.Identity;
using wu25_auction.Domain.ValueObjects;

namespace wu25_auction.Web.Data.Entities;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public Address? Address { get; private set; }

    protected ApplicationUser() { }
    
    public static ApplicationUser Create(
        string email,
        string? firstName = null,
        string? lastName = null,
        Address? address = null)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        user.SetName(firstName, lastName);
        user.SetAddress(address);

        return user;
    }

    public void Update(
        string? firstName, string lastName, Address? address)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
    }

    private void SetName(string? firstName, string? lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    private void SetAddress(Address? address)
    {
        Address = address;
    }
}