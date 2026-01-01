namespace wu25_auction.Application.DTOs;

public record AuctionItemDto(
    string Id,
    string Title,
    string Description,
    decimal CurrentPrice,
    DateTime EndDate);