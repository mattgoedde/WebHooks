using Microsoft.Extensions.DependencyInjection;
using WebHooks.Services;
using WebHooks.Services.Base;

namespace WebHooks.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWebhooks<TSubscriberUrlService>(this IServiceCollection services)
        where TSubscriberUrlService : class, ISubscriberService
    {
        return services
            .AddScoped<IActionNotifier, HttpActionNotifier>()
            .AddScoped<ISubscriberService, TSubscriberUrlService>();
    }
}