using Microsoft.AspNetCore.Mvc;
using wu25_auction.Application.DTOs.AuctionItems;
using wu25_auction.Application.Services;

namespace wu25_auction.Api.EndpointExtensions;

public static class AuctionEndpointExtensions
{
    public static IEndpointRouteBuilder MapAuctionEndpoint(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("auctions");

        group.MapGet("/", GetAllAsync);
        group.MapGet("/{id}", GetByIdAsync);
        group.MapPost("/", AddAsync);
        group.MapPut("/{id}/bid", PlaceBid);
        group.MapDelete("/{id}", DeleteAsync);
        
        return app;
    }

    private static async Task<IResult> GetAllAsync(
        [FromServices] IAuctionItemService auctionItemService)
    {
        var auctions = await auctionItemService.GetAllAsync();
        return Results.Ok(auctions);
    }

    private static async Task<IResult> GetByIdAsync(
        string id, [FromServices] IAuctionItemService auctionItemService)
    {
        var auction = await auctionItemService.GetByIdAsync(id);
        
        return Results.Ok(auction);
    }
    
    private static async Task<IResult> AddAsync(
        CreateAuctionItemDto auctionItem, [FromServices] IAuctionItemService auctionItemService)
    {
        var created = await auctionItemService.CreateAsync(auctionItem);
        
        return Results.Created(
            $"/auctions/{created.Id}", created);
    }
    
    private static async Task<IResult> PlaceBid(
        string id, PlaceBidDto bid, [FromServices] IAuctionItemService auctionItemService)
    {
        var updateAuction = await auctionItemService.PlaceBidAsync(id, bid);
        return Results.Ok(updateAuction);
    }

    private static async Task<IResult> DeleteAsync(
        string id, [FromServices] IAuctionItemService auctionItemService)
    {
        await auctionItemService.DeleteAsync(id);
        return Results.NoContent();
    }
}