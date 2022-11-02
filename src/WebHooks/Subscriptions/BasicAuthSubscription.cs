using System;
using WebHooks.Subscriptions.Base;

namespace WebHooks.Subscriptions
{
    public class BasicAuthSubscription : Subscription
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}