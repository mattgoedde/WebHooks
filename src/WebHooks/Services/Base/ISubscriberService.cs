using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebHooks.Actions.Base;
using WebHooks.Subscriptions.Base;
using WebHooks.Subscriptions;

namespace WebHooks.Services.Base
{
    public interface ISubscriberService
    {
        Task<IEnumerable<Subscription>> GetSubscribersForAction<TAction>(TAction action) where TAction : ActionBase;
    }
}
