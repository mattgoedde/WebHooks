# MGoedde.WebHooks

## A lightweight, low/zero dependency WebHook provider.

- Easy to add
- Easy to define Payload(s)
- Easy to manage subscribers
- (Optionally) Easy to secure

### 1. Implement `ISubscriberService`

This implementation will provide Subscribers each time an `IActionNotifier.NotifyAsync<TAction>()` is called.

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

This implementation defines the payload sent to subscribers.

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
