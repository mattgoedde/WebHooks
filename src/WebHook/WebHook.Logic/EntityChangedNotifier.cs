using WebHook.Class.Event;
using WebHook.Logic.Base;

namespace WebHook.Logic;

public class EntityChangedNotifier : EventNotifier<EntityChangedEvent>
{
    private readonly Dictionary<string, IEnumerable<string>> subscriberUrls = new Dictionary<string, IEnumerable<string>>()
    {
        { "Users", new string[] { "https://webhook.site/35cb204e-2dc6-4134-90e8-2297271ea71f" }}
    };

    protected override IEnumerable<string> GetSubcriberUrlsForEvent(EntityChangedEvent newEvent)
    {
        try
        {
            return subscriberUrls[newEvent.EntityType];
        }
        catch
        {
            return Enumerable.Empty<string>();
        }
    }
}