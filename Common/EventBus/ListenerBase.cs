using STAN.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Ticket.Common.EventBus
{
    abstract public class ListenerBase<T> : Event<T>
    {
        abstract public string QueGroupName { get; set; }
        abstract public void OnMessage(T _data, StanMsg msg);
        protected IStanConnection _client;
        protected int actWait = 5 * 1000;


        private StanSubscriptionOptions GetOptions()
        {
            StanSubscriptionOptions stanOptions = StanSubscriptionOptions.GetDefaultOptions();
            stanOptions.DeliverAllAvailable();
            stanOptions.AckWait = this.actWait;
            stanOptions.ManualAcks = true;
            stanOptions.DurableName = this.QueGroupName;

            return stanOptions;
        }

        public void Subscribe()
        {
            _client = NatsClient.Client;

            var _options = GetOptions();

            _client.Subscribe(this.Subject.ToString(), _options, EventHandler);
        }

        private void EventHandler(object sender, StanMsgHandlerArgs args) 
        {
            var json = System.Text.Encoding.UTF8.GetString(args.Message.Data);
            var data = JsonSerializer.Deserialize<T>(json);

            this.OnMessage(data, args.Message);
        }
    }
}
