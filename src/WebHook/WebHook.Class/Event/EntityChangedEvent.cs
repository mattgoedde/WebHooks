namespace WebHook.Class.Event;

public class EntityChangedEvent : Event
{
    public EntityChangedAction Action { get; set; } = EntityChangedAction.Updated;
    public string EntityLocation { get; set; } = "";
    public string EntityType { get; set; } = "";
}