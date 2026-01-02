using wu25_auction.Web.Infrastructure.Services;

namespace wu25_auction.Web.Infrastructure.HttpClientRegistration;

public static class HttpClientRegistration
{
    public static void AddHttpClientRegistration(
        this IServiceCollection services)
    {
        var baseUrl = new Uri("http://localhost:5067");

        services.AddHttpClient<IAuctionItemHttpClientService, AuctionItemHttpClientService>(client =>
        {
            client.BaseAddress = baseUrl;
        });
    }
}