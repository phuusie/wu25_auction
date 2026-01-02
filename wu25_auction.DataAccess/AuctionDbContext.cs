using Microsoft.EntityFrameworkCore;
using wu25_auction.Domain.Entities;

namespace wu25_auction.DataAccess;

public class AuctionDbContext(DbContextOptions<AuctionDbContext> options) : DbContext(options)
{
    public DbSet<AuctionItem> AuctionsItems => Set<AuctionItem>();
}