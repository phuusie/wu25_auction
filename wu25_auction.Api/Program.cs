using Microsoft.EntityFrameworkCore;
using wu25_auction.Api.Data.SeedData;
using wu25_auction.Api.EndpointExtensions;
using wu25_auction.Api.Middleware;
using wu25_auction.Application.Services;
using wu25_auction.DataAccess;
using wu25_auction.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseInMemoryDatabase("AuctionDb");
});

builder.Services.AddProblemDetails();

builder.Services.AddScoped<IAuctionItemRepository, AuctionItemRepository>();
builder.Services.AddScoped<IAuctionItemService, AuctionItemService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
    
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    
    await SeedData.InitializeAsync(context);
}

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.MapAuctionEndpoint();

app.Run();