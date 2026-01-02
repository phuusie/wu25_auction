using System.Text.RegularExpressions;
using wu25_auction.Domain.Entities;
using wu25_auction.Domain.Enums;

namespace wu25_auction.Tests.Domain.Entities;

public class AuctionItemTests
{
    [Fact]
    public void Generates_Id_WithCorrectFormat()
    {
        var item = new AuctionItem(
            title: "Test",
            category: "Category",
            decade: 1900,
            description: "Description",
            purchasePrice: 1000,
            startPrice: 1500,
            createdBy: "TestUser");
        
        var id = item.Id;
        
        Assert.NotNull(id);
        Assert.Matches(new Regex("^[A-Z]{3}[0-9]{6}"), id.ToString());
    }

    [Fact]
    public void WhenStartPrice_Is_LowerThenPurchesPrice_ThrowsException()
    {
        var exception = Assert.Throws<InvalidOperationException>(() =>
            new AuctionItem(
                title: "Test",
                category: "Category",
                decade: 1900,
                description: "Description",
                purchasePrice: 1000,
                startPrice: 900,
                createdBy: "TestUser"));
        
        Assert.Equal(
            "Utgångspriset måste vara högre än inköpspriset.", exception.Message);
    }

    [Fact]
    public void MarkAsSold_SetsStatusAndFinalPrice()
    {
        var item = new AuctionItem(
            title: "Test",
            category: "Category",
            decade: 1900,
            description: "Description",
            purchasePrice: 1000,
            startPrice: 1500,
            createdBy: "TestUser");
        
        item.MarkAsSold(2000);
        
        Assert.Equal(SalesItemStatus.Sold, item.Status);
        Assert.Equal(2000, item.FinalPrice);
    }

    [Fact]
    public void SetsCreatedAt()
    {
        var before = DateTime.UtcNow;

        var item = new AuctionItem(
            title: "Test",
            category: "Category",
            decade: 1900,
            description: "Description",
            purchasePrice: 1000,
            startPrice: 1500,
            createdBy: "TestUser");
        
        var after = DateTime.UtcNow;
        
        Assert.InRange(item.CreatedAt, before, after);
    }
}