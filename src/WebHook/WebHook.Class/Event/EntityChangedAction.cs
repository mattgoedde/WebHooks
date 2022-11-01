using System.Text.Json.Serialization;

namespace WebHook.Class.Event;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EntityChangedAction
{
    Created,
    Updated,
    Deleted
}