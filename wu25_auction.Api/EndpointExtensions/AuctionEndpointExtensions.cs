using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using wu25_auction.Application.DTOs.AuctionItems;
using wu25_auction.Application.Services;

namespace wu25_auction.Api.EndpointExtensions;

public static class AuctionEndpointExtensions
{
    public static IEndpointRouteBuilder MapAuctionEndpoint(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auctions");

        group.MapGet("/", GetAllAsync);
        group.MapGet("/{id}", GetByIdAsync);
        group.MapPost("/", AddAsync);
        group.MapPut("{id}/sold", MarkAsSold);
        
        return app;
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
        
        return Results.Ok(item);
    }
    
    private static async Task<IResult> AddAsync(
        AuctionItemDto dto, ClaimsPrincipal user, [FromServices] IAuctionItemService auctionItemService)
    {
        // var userId = user.Identity!.Name!;
        var tempId = "anonymous-user";
        var id = await auctionItemService.CreateAsync(dto, tempId);
        
        return Results.Created(
            $"/auctions/{id}", null);
    }

    private static async Task<IResult> MarkAsSold(
        string id, decimal finalPrice, ClaimsPrincipal user, [FromServices] IAuctionItemService auctionItemService)
    {
        var isManager = user.IsInRole("Manager");
        
        await auctionItemService.MarkAsSoldAsync(id, finalPrice, isManager);
        
        return Results.NoContent();
    }
}