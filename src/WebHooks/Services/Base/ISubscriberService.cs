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
        Task<IEnumerable<Subscription>> GetUnauthenticatedSubcribersForAction<TAction>(TAction action) where TAction : ActionBase;
        Task<IEnumerable<BasicAuthSubscription>> GetBasicAuthSubcribersForAction<TAction>(TAction action) where TAction : ActionBase;
        Task<IEnumerable<OAuthSubscription>> GetOAuthSubcribersForAction<TAction>(TAction action) where TAction : ActionBase;
    }
}
