using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using WebHooks.Actions.Base;
using WebHooks.Services.Base;
using WebHooks.Subscriptions.Base;
using WebHooks.Subscriptions;

namespace WebHooks.Services
{
    public class HttpActionNotifier : IActionNotifier
    {
        private readonly ISubscriberService _subscribers;

        public HttpActionNotifier(ISubscriberService subscribers)
        {
            _subscribers = subscribers;
        }

        public async Task Notify<TAction>(TAction action) where TAction : ActionBase
        {
            List<Task> eventTasks = new List<Task>();

            foreach (var subscription in await _subscribers.GetUnauthenticatedSubcribersForAction(action))
            {
                eventTasks.Add(Send(action, subscription));
            }
            foreach (var subscription in await _subscribers.GetBasicAuthSubcribersForAction(action))
            {
                eventTasks.Add(Send(action, subscription));
            }
            foreach (var subscription in await _subscribers.GetOAuthSubcribersForAction(action))
            {
                eventTasks.Add(Send(action, subscription));
            }

            await Task.WhenAll(eventTasks);
        }

        private Task Send<TAction>(TAction action, Subscription subscription) where TAction : ActionBase
        {
            using var httpClient = new HttpClient();
            return httpClient.PostAsJsonAsync(subscription.Endpoint, action);
        }
        private Task Send<TAction>(TAction action, BasicAuthSubscription subscription) where TAction : ActionBase
        {
            using var httpClient = new HttpClient();
            return httpClient.PostAsJsonAsync(subscription.Endpoint, action);
        }
        private Task Send<TAction>(TAction action, OAuthSubscription subscription) where TAction : ActionBase
        {
            using var httpClient = new HttpClient();
            return httpClient.PostAsJsonAsync(subscription.Endpoint, action);
        }
    }
}
