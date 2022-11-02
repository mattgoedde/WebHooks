using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebHooks.Subscriptions;
using WebHooks.Actions;

namespace WebHooks.Services.Base
{
    public interface ISubscriberService
    {
        Task<IEnumerable<Subscription>> GetSubscribersForAction<TAction>(TAction action) where TAction : BaseAction;
    }
}
