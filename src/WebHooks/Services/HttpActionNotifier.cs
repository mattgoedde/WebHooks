using System.Net.Http.Json;
using WebHooks.Actions.Base;
using WebHooks.Services.Base;

namespace WebHooks.Services;

public class HttpActionNotifier : IActionNotifier
{
    private readonly ISubscriberService _subscribers;

    public HttpActionNotifier(ISubscriberService subscribers)
    {
        _subscribers = subscribers;
    }

    public async Task Notify<TAction>(TAction action) where TAction : ActionBase
    {
        using var httpClient = new HttpClient();

        List<Task> eventTasks = new();

        foreach (var url in await _subscribers.GetSubcribersForAction(action))
        {
            eventTasks.Add(httpClient.PostAsJsonAsync(url, action));
        }

        await Task.WhenAll(eventTasks);
    }
}