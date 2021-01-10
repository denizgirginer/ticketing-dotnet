using NATS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_demo_api.Streaming
{
    public static class NatsClient
    {
        public static IConnection GetClient()
        {
            ConnectionFactory factory = new ConnectionFactory();

            var options = ConnectionFactory.GetDefaultOptions();
            options.Url = "nats://localhost:4222";

            return factory.CreateConnection(options);
        }

        public static void PubSub()
        {
            var cli = GetClient();

            //cli.Publish()
        }
    }
}
