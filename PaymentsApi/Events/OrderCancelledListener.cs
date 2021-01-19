using PaymentsApi.Repo;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;
using Ticket.Common.Events;

namespace PaymentsApi.Events
{
    public class OrderCancelledListener : ListenerBase<OrderCancelledEvent, OrderCancelledData>, IOrderCancelledListener
    {
        public override Subjects Subject => Subjects.OrderCancelled;

        public override string QueGroupName => QueGroups.PaymentsService;

        IOrderRepo _orderRepo;
        public OrderCancelledListener(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public override async void OnMessage(OrderCancelledData _data, StanMsg msg)
        {
            var order = await _orderRepo
                .GetAsync(x=>x.id==_data.id && x.version==_data.version-1);

            if(order==null)
            {
                throw new Exception("Order Not Found");
            }

            order.status = OrderStatus.Cancelled;
            await _orderRepo.UpdateAsync(order.id, order);

            msg.Ack();
        }
    }
}
