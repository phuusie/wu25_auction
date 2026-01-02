using wu25_auction.Web.Infrastructure.Services;

namespace wu25_auction.Web.Infrastructure.HttpClientRegistration;

public static class HttpClientRegistration
{
    public static IServiceCollection AddHttpClientRegistration(
        this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = new Uri("http://localhost:5067");

        services.AddHttpClient<IAuctionItemHttpClientService, AuctionItemHttpClientService>(client =>
        {
            client.BaseAddress = baseUrl;
        });
        
        return services;
    }
}