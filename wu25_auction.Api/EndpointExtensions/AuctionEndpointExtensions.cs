using wu25_auction.Application.DTOs.AuctionItems;
using wu25_auction.Application.Services;

namespace wu25_auction.Api.EndpointExtensions;

public static class AuctionEndpointExtensions
{
    public static IEndpointRouteBuilder MapAuctionEndpoint(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("auctions");

        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
        group.MapPost("/", AddAsync);
        group.MapPut("/{id}/bid", PlaceBid);
        
        return app;
    }

    private static async Task<IResult> GetAll(
        IAuctionItemService auctionItemService)
    {
        var auctions = await auctionItemService.GetAllAsync();
        return Results.Ok(auctions);
    }

    private static async Task<IResult> GetById(
        string id, IAuctionItemService auctionItemService)
    {
        var auction = await auctionItemService.GetByIdAsync(id);

        if (auction == null)
            return Results.NotFound();
        
        return Results.Ok(auction);
    }
    
    private static async Task<IResult> AddAsync(
        CreateAuctionItemDto auctionItem, IAuctionItemService auctionItemService)
    {
        var created = await auctionItemService.CreateAsync(auctionItem);
        
        return Results.Created(
            $"/auctions/{created.Id}", created);
    }
    
    private static async Task<IResult> PlaceBid(
        string id, PlaceBidDto bid, IAuctionItemService auctionItemService)
    {
        var updateAuction = await auctionItemService.PlaceBidAsync(id, bid);
        return Results.Ok(updateAuction);
    }
}