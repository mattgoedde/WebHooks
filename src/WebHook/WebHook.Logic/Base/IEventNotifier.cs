using WebHook.Class.Event;

namespace WebHook.Logic.Base;

public interface IEventNotifier<TEvent>
    where TEvent : Event
{
    Task Notify(TEvent newEvent);
}