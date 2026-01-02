namespace wu25_auction.Web.Models;

public class AuctionItemViewModel
{
    public string Id { get; init; } = default!;
    public string Category { get; init; } = default!;
    public int Decade { get; init; }
    public string Description { get; init; } = default!;

    public decimal StartPrice { get; init; }
    public decimal? FinalPrice { get; init; }

    public bool IsSold { get; init; }
}