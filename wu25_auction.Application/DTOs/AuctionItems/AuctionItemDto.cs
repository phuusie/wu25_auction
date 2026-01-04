using wu25_auction.Domain.Enums;

namespace wu25_auction.Application.DTOs.AuctionItems;

public class AuctionItemDto
{
    public string Id { get; init; } = "";
    public string Title { get; init; } = "";
    public string Category { get; init; } = "";
    public int Decade { get; init; }
    public string Description { get; init; } = "";

    public decimal PurchasePrice { get; init; }
    public decimal StartPrice { get; init; }
    public decimal? FinalPrice { get; init; }

    public SalesItemStatus Status { get; init; }
    
    public string CreatedBy { get; init; } = "";

    public string? ImageUrl { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime EndsAt { get; init; }
}
