using WebHooks.Actions;
using WebHooks.Services.Base;
using WebHooks.Subscriptions;

namespace WebHooks.Console;

public class SubscriberService : ISubscriberService
{
    public async Task<IEnumerable<Subscription>> GetSubscribersForAction<TAction>(TAction action)
        where TAction : BaseAction
    {
        await Task.CompletedTask;
        return new List<Subscription>()
        {
            new Subscription()
            {
                Endpoint = new Uri(""),
                SecretToken = ""
            }
        };
    }
}