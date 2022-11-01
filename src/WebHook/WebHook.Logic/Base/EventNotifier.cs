using System.Net.Http.Json;
using WebHook.Class.Event;

namespace WebHook.Logic.Base;

public abstract class EventNotifier<TEvent> : IEventNotifier<TEvent>
    where TEvent : Event
{
    protected abstract IEnumerable<string> GetSubcriberUrlsForEvent(TEvent newEvent);

    public async Task Notify(TEvent newEvent)
    {
        using var httpClient = new HttpClient();

        List<Task> eventTasks = new();

        foreach (var url in GetSubcriberUrlsForEvent(newEvent))
        {
            eventTasks.Add(httpClient.PostAsJsonAsync(url, newEvent));
        }

        await Task.WhenAll(eventTasks);
    }
}