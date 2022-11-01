namespace WebHook.Class.Event;

public abstract class Event
{
    public DateTime EventTimestampUtc { get; set; } = DateTime.UtcNow;
}