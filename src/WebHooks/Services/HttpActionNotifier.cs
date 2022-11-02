using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Net.Http;
using WebHooks.Services.Base;
using WebHooks.Subscriptions;
using System.Text;
using System.Security.Cryptography;
using System.Text.Json;
using WebHooks.Actions;

namespace WebHooks.Services
{
    public class HttpActionNotifier : IActionNotifier
    {
        private readonly ISubscriberService _subscribers;
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = false
        };

        public HttpActionNotifier(ISubscriberService subscribers)
        {
            _subscribers = subscribers;
        }

        public async Task NotifyAsync<TAction>(TAction action) where TAction : BaseAction
        {
            List<Task> eventTasks = new List<Task>();

            foreach (var subscription in await _subscribers.GetSubscribersForAction(action))
            {
                eventTasks.Add(Send(action, subscription));
            }

            await Task.WhenAll(eventTasks);
        }

        private async Task Send<TAction>(TAction action, Subscription subscription) where TAction : BaseAction
        {
            using var httpClient = new HttpClient();

            string actionJson = JsonSerializer.Serialize(action, options: jsonOptions);

            if (!string.IsNullOrWhiteSpace(subscription.SecretToken))
            {
                // Convert action to byte array in order to compute the hash
                byte[] actionBytes = Encoding.UTF8.GetBytes(actionJson);

                // Convert secret token to byte array - for initializing HMACSHA256
                byte[] secretTokenBytes = Convert.FromBase64String(subscription.SecretToken);

                using HMACSHA256 hmac = new HMACSHA256(secretTokenBytes);

                // Compute signature hash
                byte[] actionHasBytes = hmac.ComputeHash(actionBytes);

                // Convert signature hash to Base64String for transmission
                string signature = Convert.ToBase64String(actionHasBytes);

                httpClient.DefaultRequestHeaders.Add("x-hook-signature-256", signature);
            }

            var content = new StringContent(actionJson, Encoding.UTF8, "application/json");
            await httpClient.PostAsync(subscription.Endpoint, content);
        }
    }
}
