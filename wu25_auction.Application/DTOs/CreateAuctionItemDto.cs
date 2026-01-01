namespace wu25_auction.Application.DTOs;

public record CreateAuctionItemDto(
    string Title, 
    string Description,
    decimal StartPrice, 
    DateTime EndDate);