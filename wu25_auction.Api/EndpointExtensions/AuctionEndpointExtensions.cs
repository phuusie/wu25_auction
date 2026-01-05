using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using wu25_auction.Application.DTOs.AuctionItems;
using wu25_auction.Application.Services;

namespace wu25_auction.Api.EndpointExtensions;

public static class AuctionEndpointExtensions
{
    public static void MapAuctionEndpoint(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auctions");

        group.MapGet("/", GetAllAsync);
        group.MapGet("/{id}", GetByIdAsync);
        group.MapGet("/inventory/{userId}", GetAllByUserIdAsync);
        group.MapPost("/", AddAsync);
        group.MapPut("{id}/sold", MarkAsSold);
    }

    private static async Task<IResult> GetAllAsync(
        [FromServices] IAuctionItemService auctionItemService)
    {
        var items = await auctionItemService.GetAllAsync();
        return Results.Ok(items);
    }

    private static async Task<IResult> GetByIdAsync(
        string id, [FromServices] IAuctionItemService auctionItemService)
    {
        var item = await auctionItemService.GetByIdAsync(id);
        
        if (item == null)
            return Results.NotFound();
        
        return Results.Ok(item);
    }

    private static async Task<IResult> GetAllByUserIdAsync(
        [FromServices] IAuctionItemService auctionItemService, string userId)
    {
        var items = await auctionItemService.GetAllByUserIdAsync(userId);
        return Results.Ok(items);
    }
    
    private static async Task<IResult> AddAsync(
        [FromBody] AuctionItemDto dto, [FromQuery] string user, [FromServices] IAuctionItemService auctionItemService)
    {
        var id = await auctionItemService.CreateAsync(dto, user);
        
        return Results.Ok(id);
    }
    
    private static async Task<IResult> MarkAsSold(
        string id, decimal finalPrice, ClaimsPrincipal user, [FromServices] IAuctionItemService auctionItemService)
    {
        var isManager = user.IsInRole("Manager");
        
        await auctionItemService.MarkAsSoldAsync(id, finalPrice, isManager);
        
        return Results.NoContent();
    }
}