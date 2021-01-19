using PaymentsApi.Models;
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
    public class OrderCreatedListener : ListenerBase<OrderCreatedEvent, OrderCreatedData>, IOrderCreatedListener
    {
        public override Subjects Subject => Subjects.OrderCreated;

        public override string QueGroupName => QueGroups.PaymentsService;

        IOrderRepo _orderRepo;
        public OrderCreatedListener(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public override async void OnMessage(OrderCreatedData _data, StanMsg msg)
        {
            var order = new Order()
            {
                id = _data.id,
                price = _data.ticket.price,
                status = _data.status,
                userId = _data.userId,
                version = _data.version
            };
            await _orderRepo.AddAsync(order);

            msg.Ack();
        }
    }
}
