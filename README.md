# WebHooks

```csharp
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
```

```csharp
public class MessageAction : BaseAction
{
    public string Message { get; set; } = string.Empty;
}
```

```csharp
services.AddWebHooks<SubscriberService>();
```

```csharp
public class WeatherController : ControllerBase
{
    private readonly IActionNotifier _actionNotifier;

    public WeatherController(IActionNotifier actionNotifier)
    {
        _actionNotifier = actionNotifier;
    }
}
```

```csharp
await _actionNotifier.NotifyAsync(new MessageAction() { "Hello, WebHook!" });
```
