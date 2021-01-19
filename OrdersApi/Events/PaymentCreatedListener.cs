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
    public class PaymentCreatedListener : ListenerBase<PaymentCreatedEvent, PaymentCreatedData>, IPaymentCreatedListener
    {
        private IOrderRepo _orderRepo;

        public override Subjects Subject => Subjects.PaymentCreated;

        public override string QueGroupName => QueGroups.OrdersService;

        public PaymentCreatedListener(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public override async void OnMessage(PaymentCreatedData _data, StanMsg msg)
        {
            var order = await _orderRepo.GetByIdAsync(_data.orderId);

            if(order==null)
            {
                throw new Exception("Order Not Found");
            }

            order.status = OrderStatus.Complete;
            await _orderRepo.UpdateAsync(order.id, order);

            //no publish needed because order completed

            msg.Ack();
        }
    }
}
