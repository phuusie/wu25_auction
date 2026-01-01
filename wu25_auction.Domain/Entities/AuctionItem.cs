using wu25_auction.Domain.Entities.Common;

namespace wu25_auction.Domain.Entities;

public class AuctionItem : Entity<string>
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal StartPrice { get; private set; }
    public decimal CurrentPrice { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    
    public AuctionItem(
        string title, string description, decimal startPrice, DateTime endDate)
    {
        
        if (startPrice <= 0)
            throw new ArgumentException("Startpris måste vara större än 0.");

        if (endDate <= DateTime.UtcNow)
            throw new ArgumentException("Slutdatum måste vara i framtiden.");

        Id = Guid.NewGuid().ToString();
        Title = title;
        Description = description;
        StartPrice = startPrice;
        CurrentPrice = startPrice;
        StartDate = DateTime.UtcNow;
        EndDate = endDate;
    }

    public void PlaceBid(decimal amount)
    {
        if (amount <= CurrentPrice)
            throw new InvalidOperationException("Budet måste vara högre än nuvarande pris.");

        if (DateTime.UtcNow > EndDate)
            throw new InvalidOperationException("Auktionen är avslutad.");

        CurrentPrice = amount;
    }
}