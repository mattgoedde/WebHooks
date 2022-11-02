using System;

namespace WebHooks.Subscriptions
{
    public class Subscription
    {
        public Uri Endpoint { get; set; }
        public string SecretToken { get; set; } = string.Empty;
    }
}