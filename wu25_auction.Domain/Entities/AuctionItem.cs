using System.Security.Cryptography;
using System.Text;
using wu25_auction.Domain.Entities.Common;
using wu25_auction.Domain.Enums;

namespace wu25_auction.Domain.Entities;

public class AuctionItem : Entity<string>
{
    public string Category { get; private set; }
    public int Decade { get; private set; }
    public string Description { get; private set; }
    
    public decimal PurchasePrice { get; private set; }
    public decimal StartPrice { get; private set; }
    public decimal? FinalPrice { get; private set; }
    
    public SalesItemStatus Status { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private AuctionItem() { }

    public AuctionItem(
        string category, int decade, string description, decimal purchasePrice, decimal startPrice,
        string createdBy)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Kategori måste anges.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Beskrining måste anges.");
        
        if (purchasePrice < 0)
            throw new ArgumentException("Inköpspriset kan inte vara negativt.");
        
        if (startPrice < purchasePrice)
            throw new InvalidOperationException("Utgångspriset måste vara högre än inköpspriset.");

        if (decade > DateTime.UtcNow.Year)
            throw new ArgumentException("Ogiltigt årtione.");

        Id = GenerateId();
        Category = category;
        Decade = decade;
        Description = description;
        PurchasePrice = purchasePrice;
        StartPrice = startPrice;
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
        Status = SalesItemStatus.InStock;
    }

    public void MarkAsSold(decimal finalPrice)
    {
        if (Status == SalesItemStatus.Sold)
            throw new InvalidOperationException("Objektet är redan sålt.");

        if (finalPrice <= 0)
            throw new InvalidOperationException("Slutpriset måste vara högre än 0.");

        Status = SalesItemStatus.Sold;
        FinalPrice = finalPrice;
    }

    private static string GenerateId()
    {
        const string letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";

        var result = new StringBuilder(9);

        for (int i = 0; i < 3; i++)
        {
            int index = RandomNumberGenerator.GetInt32(letter.Length);
            result.Append(letter[index]);
        }

        for (int i = 0; i < 6; i++)
        {
            int index = RandomNumberGenerator.GetInt32(digits.Length);
            result.Append(digits[index]);
        }
        
        return result.ToString();
    }
}