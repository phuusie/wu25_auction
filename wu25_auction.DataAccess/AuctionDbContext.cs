using Microsoft.EntityFrameworkCore;
using wu25_auction.Domain.Entities;

namespace wu25_auction.DataAccess;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(
        DbContextOptions<AuctionDbContext> options) : base(options)
    {
        
    }

    public DbSet<AuctionItem> AuctionsItems => Set<AuctionItem>();
}