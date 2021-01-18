using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ticket.Common.EventBus
{
    public static class NatsClient
    {
        private static IStanConnection _client;
        public static void AddNats(this IServiceCollection services)
        {
            var cf = new StanConnectionFactory();
            var options = StanOptions.GetDefaultOptions();

            var NATS_URL = Environment.GetEnvironmentVariable("NATS_URL");
            var NATS_CLUSTER = Environment.GetEnvironmentVariable("NATS_CLUSTER");
            var NATS_CLIENT_ID = Environment.GetEnvironmentVariable("NATS_CLIENT_ID");

            options.NatsURL = NATS_URL;


            _client = cf.CreateConnection(NATS_CLUSTER, NATS_CLIENT_ID, options);
        }

        public static IStanConnection Client { get {return _client;} }


        public static async Task<string> Publish<T>(Event<T> _event)
        {
            string eventData = JsonSerializer.Serialize(_event.Data);

            // create message
            // Event-type is embedded in the message:
            //   <event-type>#<value>|<value>|<value>
            string body = $"{eventData}";
            byte[] message = Encoding.UTF8.GetBytes(body);

            string subject = _event.Subject.ToString();
            
            return await _client.PublishAsync(subject,  message);
        }

    }

    
}
