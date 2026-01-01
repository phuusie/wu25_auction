using Microsoft.EntityFrameworkCore;
using wu25_auction.Api.EndpointExtensions;
using wu25_auction.DataAccess;
using wu25_auction.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseInMemoryDatabase("AuctionDb");
});

builder.Services.AddScoped<IAuctionItemRepository, AuctionItemRepository>();

var app = builder.Build();

app.MapAuctionEndpoint();

app.Run();