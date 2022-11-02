using WebHooks.Actions;
using WebHooks.Services.Base;
using WebHooks.Subscriptions;

namespace WebHooks.Console;

public class SubscriberService : ISubscriberService
{
    public async Task<IEnumerable<Subscription>> GetSubscribersForAction<TAction>(TAction action) where TAction : BaseAction
    {
        await Task.CompletedTask;
        return new List<Subscription>()
        {
            new Subscription() { Endpoint = new Uri("https://webhook.site/182a7f5d-b779-4be5-b04c-69abac568265"), SecretToken = "t9GmVMWUU2N8nZSRg480rEOEc9G2UF90" }
        };
    }
}