using wu25_auction.Application.DTOs.AuctionItems;
using wu25_auction.DataAccess.Repositories;
using wu25_auction.Domain.Entities;

namespace wu25_auction.Application.Services;

public class AuctionItemService(IAuctionItemRepository itemRepository) : IAuctionItemService
{
    public async Task<string> CreateAsync(AuctionItemDto dto, string userId)
    {
        var item = new AuctionItem(
            dto.Title, dto.Category, dto.Decade, dto.Description, dto.PurchasePrice, dto.StartPrice, userId);
        
        await itemRepository.AddAsync(item);

        return item.Id;
    }

    public async Task<IEnumerable<AuctionItem>> GetAllAsync()
    {
        return await itemRepository.GetAllAsync();
    }

    public async Task<AuctionItem?> GetByIdAsync(string id)
    {
        return await itemRepository.GetByIdAsync(id);
    }

    public Task<IEnumerable<AuctionItem>> GetAllByUserIdAsync(string userId)
    {
        return itemRepository.GetAllByUserIdAsync(userId);
    }

    public async Task MarkAsSoldAsync(string id, decimal price, bool isManager)
    {
        if (!isManager)
            throw new UnauthorizedAccessException(
                "Endast auktionsansvariga får markera objektet som såld.");

        var item = await itemRepository.GetByIdAsync(id)
                   ?? throw new KeyNotFoundException("Objektet finns inte.");
        
        item.MarkAsSold(price);
        
        await itemRepository.SaveChangesAsync();
    }
}