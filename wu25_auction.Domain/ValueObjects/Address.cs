using wu25_auction.Domain.ValueObjects.Common;

namespace wu25_auction.Domain.ValueObjects;

public sealed class Address : ValueObject
{
    public string Street { get; init; } = string.Empty;
    public string City { get; init;  } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    
    private Address() {}

    public Address(string street, string city, string postalCode, string country)
    {
        Street = street;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return PostalCode;
        yield return Country;
    }
}