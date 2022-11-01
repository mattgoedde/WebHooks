using WebHooks.Actions.Base;

namespace WebHooks.Services.Base;

public interface ISubscriberService
{
    Task<IEnumerable<Uri>> GetSubcribersForAction<TAction>(TAction action) where TAction : ActionBase;
}