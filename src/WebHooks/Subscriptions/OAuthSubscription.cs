using System;
using WebHooks.Subscriptions.Base;

namespace WebHooks.Subscriptions
{
    public class OAuthSubscription : Subscription
    {
        public string AppId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}