using ExpirationSvc.JobQue;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;
using Ticket.Common.Events;

namespace ExpirationSvc.Events
{
    public class OrderCreatedListener : ListenerBase<OrderCreatedEvent, OrderCreatedData>, IOrderCreatedListener
    {
        IOrderExpirationQue _orderExpirationQue;

        public override Subjects Subject => Subjects.OrderCreated;

        public override string QueGroupName => QueGroups.ExpirationSvc;

        public OrderCreatedListener(IOrderExpirationQue orderExpirationQue)
        {
            _orderExpirationQue = orderExpirationQue;
        }

        public OrderCreatedListener()
        {
            
        }

        public override async void OnMessage(OrderCreatedData _data, StanMsg msg)
        {
            Console.WriteLine("Order Created Received:" + _data.id);

            string orderId = _data.id;
            await _orderExpirationQue.AddScheduledTask(() => DoExpire(orderId), DateTime.Now.AddMinutes(2));

            msg.Ack();
        }

        private async void DoExpire(string orderId)
        {
            Console.WriteLine("Order Expired:" + orderId);
            var evt = new ExpirationCompleteEvent();
            evt.Data.orderId = orderId;
            await evt.Publish();
        }
    }
}
