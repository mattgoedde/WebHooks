using WebHooks.Actions;

namespace WebHooks.Console;

public class MessageAction : BaseAction
{
    public string Message { get; set; } = string.Empty;
}