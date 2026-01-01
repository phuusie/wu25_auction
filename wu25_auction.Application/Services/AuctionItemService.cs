using wu25_auction.Application.DTOs;
using wu25_auction.DataAccess.Repositories;
using wu25_auction.Domain.Entities;

namespace wu25_auction.Application.Services;

public class AuctionItemService : IAuctionItemService
{
    private readonly IAuctionItemRepository _auctionItemRepository;
    
    public AuctionItemService(IAuctionItemRepository itemRepository)
    {
        _auctionItemRepository = itemRepository;
    }
    
    public async Task<IEnumerable<AuctionItemDto>> GetAllAsync()
    {
        var auctions = await _auctionItemRepository.GetAllAsync();

        return auctions.Select(a => new AuctionItemDto(
            a.Id,
            a.Title,
            a.Description,
            a.CurrentPrice,
            a.EndDate));
    }

    public async Task<AuctionItemDto?> GetByIdAsync(int id)
    {
        var auction = await _auctionItemRepository.GetByIdAsync(id);
        if (auction == null)
            return null;
        
        return new AuctionItemDto(
            auction.Id,
            auction.Title,
            auction.Description,
            auction.CurrentPrice,
            auction.EndDate);
    }

    public async Task<AuctionItemDto> CreateAsync(CreateAuctionItemDto auctionItem)
    {
       var auction = new AuctionItem(
            auctionItem.Title,
            auctionItem.Description,
            auctionItem.StartPrice,
            auctionItem.EndDate);
        
        var created = await _auctionItemRepository.AddAsync(auction);
        
        return new AuctionItemDto(
            created.Id,
            created.Title,
            created.Description,
            created.CurrentPrice,
            created.EndDate);
    }
}