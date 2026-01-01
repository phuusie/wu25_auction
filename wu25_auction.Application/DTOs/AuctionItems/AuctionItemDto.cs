namespace wu25_auction.Application.DTOs.AuctionItems;

public record AuctionItemDto(
    string Category, int Decade, string Description, decimal PurchasePrice, decimal StartPrice);