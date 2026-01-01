using wu25_auction.Application.DTOs.AuctionItems;
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
        var items = await _auctionItemRepository.GetAllAsync();

        return items.Select(a => new AuctionItemDto(
            a.Id, a.Title, a.Description, a.CurrentPrice, a.EndDate));
    }

    public async Task<AuctionItemDto?> GetByIdAsync(string id)
    {
        var item = await _auctionItemRepository.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException("Auktionen finns inte.");
        
        return new AuctionItemDto(
            item.Id, item.Title, item.Description, item.CurrentPrice, item.EndDate);
    }

    public async Task<AuctionItemDto> CreateAsync(CreateAuctionItemDto item)
    {
        var auctionItem = new AuctionItem(
            item.Title, item.Description, item.StartPrice, item.EndDate);
        
        var created = await _auctionItemRepository.AddAsync(auctionItem);
        
        return new AuctionItemDto(
            created.Id, created.Title, created.Description, created.CurrentPrice, created.EndDate);
    }

    public async Task<AuctionItemDto> PlaceBidAsync(string id, PlaceBidDto bid)
    {
        var item = await _auctionItemRepository.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException("Auktionen finns inte.");
        
        item.PlaceBid(bid.amount);

        await _auctionItemRepository.SaveChangesAsync();
        
        return new AuctionItemDto(
            item.Id, item.Title, item.Description, item.CurrentPrice, item.EndDate);
    }

    public async Task DeleteAsync(string id)
    {
        var item = await _auctionItemRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Auktionen finns inte.");
        
        await _auctionItemRepository.DeleteAsync(item);
        await _auctionItemRepository.SaveChangesAsync();
    }
}