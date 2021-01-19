using OrdersApi.Repo;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;
using Ticket.Common.Events;

namespace OrdersApi.Events
{
    public class ExpirationCompleteListener : ListenerBase<ExpirationCompleteEvent, ExpirationCompleteData>, IExpirationCompleteListener
    {
        public override Subjects Subject => Subjects.ExpirationComplete;

        public override string QueGroupName => QueGroups.OrdersService;

        IOrderRepo _orderRepo;
        public ExpirationCompleteListener(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public override async void OnMessage(ExpirationCompleteData _data, StanMsg msg)
        {
            var order = await _orderRepo.GetOrderById(_data.orderId);

            if(order==null)
            {
                throw new Exception("Order Not Found");
            }

            //if order Completed return ack
            if (order.status == OrderStatus.Complete)
            {
                msg.Ack(); return;
            }

            order.status = OrderStatus.Cancelled;
            await _orderRepo.UpdateAsync(order.id, order);

            //publish event OrderCancelled
            var evt = new OrderCancelledEvent();
            evt.Data.id = order.id;
            evt.Data.version = order.version;
            evt.Data.ticket.id = order.ticket.id;
            await evt.Publish();

            msg.Ack();
        }
    }
}
