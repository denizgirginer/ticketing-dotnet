using STAN.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;
using Ticket.Common.Events;
using TicketsApi.Repo;

namespace TicketsApi.Events
{
    public class OrderCreatedListener : ListenerBase<OrderCreatedEvent, OrderCreatedData>, IOrderCreatedListener
    {
        private ITicketRepo _ticketRepo;

        public override Subjects Subject => Subjects.OrderCreated;

        public override string QueGroupName => QueGroups.TicketsService;

        public OrderCreatedListener(ITicketRepo ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public override async void OnMessage(OrderCreatedData _data, StanMsg msg)
        {
            var ticket = await _ticketRepo.GetByIdAsync(_data.ticket.id);

            if(ticket==null)
            {
                throw new Exception("Ticket not found");
            }

            ticket.orderId = _data.id;
            await _ticketRepo.UpdateAsync(ticket.id, ticket);

            var evt = new TicketUpdatedEvent();
            evt.Data.price = ticket.price;
            evt.Data.title = ticket.title;
            evt.Data.userId = ticket.userId;
            evt.Data.version = ticket.version;
            evt.Data.id = ticket.id;
            evt.Data.orderId = ticket.orderId;
            await evt.Publish();

            msg.Ack();
        }
    }
}
