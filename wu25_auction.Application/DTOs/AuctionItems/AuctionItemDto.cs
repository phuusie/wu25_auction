namespace wu25_auction.Application.DTOs.AuctionItems;

public record AuctionItemDto(
    string Id, string Title, string Description, decimal CurrentPrice, DateTime EndDate);