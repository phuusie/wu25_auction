using wu25_auction.Domain.Enums;

namespace wu25_auction.Application.DTOs.AuctionItems;

public record AuctionItemDto(
    string Id, string Category, int Decade, string Description, decimal PurchasePrice, decimal StartPrice, decimal? FinalPrice, SalesItemStatus Status);