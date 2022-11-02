# WebHooks

### 1. Implement `ISubscriberService`

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

### 2. Implement `BaseAction`

```csharp
public class MessageAction : BaseAction
{
    public string Message { get; set; } = string.Empty;
}
```

3. Call `AddWebHooks<TSubscriberService>()` to add `IActionNotifier` to DI Container

```csharp
services.AddWebHooks<SubscriberService>();
```

### 4. Inject `IActionNotifier` into your class

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

### 5. Call `IActionNotifier.NotifyAsync<TAction>()` in your code

```csharp
await _actionNotifier.NotifyAsync(new MessageAction() { "Hello, WebHook!" });
```
