namespace wu25_auction.Application.DTOs.AuctionItems;

public record CreateAuctionItemDto(
    string Title, string Description, decimal StartPrice, DateTime EndDate);