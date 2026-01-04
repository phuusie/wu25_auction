namespace wu25_auction.Web.Models;

public class AuctionItemViewModel
{
    public string Id { get; init; } = default!;
    public string Title { get; init; }
    public string Category { get; init; } = default!;
    public int Decade { get; init; }
    public string Description { get; init; } = default!;
    
    public decimal PurchasePrice { get; init; }
    public decimal StartPrice { get; init; }
    public decimal? FinalPrice { get; init; }
    
    public DateTime CreatedAt { get; init; }
    public DateTime EndsAt { get; init; }

    public bool IsSold { get; init; }
    
    public string? ImageUrl { get; init; }
}