using wu25_auction.Application.DTOs;
using wu25_auction.Application.Services;

namespace wu25_auction.Api.EndpointExtensions;

public static class AuctionEndpointExtensions
{
    public static IEndpointRouteBuilder MapAuctionEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("auctions");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", AddAsync);
        
        return app;
    }

    private static async Task<IResult> GetAll(IAuctionItemService auctionItemService)
    {
        var auctions = await auctionItemService.GetAllAsync();
        return Results.Ok(auctions);
    }

    private static async Task<IResult> GetById(int id, IAuctionItemService auctionItemService)
    {
        var auction = await auctionItemService.GetByIdAsync(id);

        if (auction == null)
            return Results.NotFound();
        
        return Results.Ok(auction);
    }
    
    private static async Task<IResult> AddAsync(CreateAuctionItemDto auctionItem, IAuctionItemService auctionItemService)
    {
        var createdAuction = await auctionItemService.CreateAsync(auctionItem);
        
        return Results.Created(
            $"/auctions/{createdAuction.Id}", 
            createdAuction);
    }
}