using STAN.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Ticket.Common.Events;

namespace Ticket.Common.EventBus
{
    abstract public class ListenerBase<T, TData> where T:Event<TData>
    {
        abstract public Subjects Subject { get; }
        abstract public string QueGroupName { get; }
        abstract public void OnMessage(TData _data, StanMsg msg);
        protected IStanConnection _client;
        protected int actWait = 5 * 1000;

        public ListenerBase(){
            
        }

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

            var subjectName = this.Subject.ToString();

            Console.WriteLine(subjectName+ " subscribed");

            _client.Subscribe(subjectName, _options, EventHandler);
        }

        private void EventHandler(object sender, StanMsgHandlerArgs args) 
        {
            var json = System.Text.Encoding.UTF8.GetString(args.Message.Data);

            Console.WriteLine("Handling:"+args.Message.Subject);
            Console.WriteLine(json);

            var data = JsonSerializer.Deserialize<TData>(json);

            this.OnMessage(data, args.Message);
        }
    }
}
