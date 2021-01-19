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
    public class OrderCancelledListener : ListenerBase<OrderCancelledEvent, OrderCancelledData>, IOrderCancelledListener
    {
        private ITicketRepo _ticketRepo;

        public override Subjects Subject => Subjects.OrderCancelled;

        public override string QueGroupName => QueGroups.TicketsService;

        public OrderCancelledListener(ITicketRepo ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public override async void OnMessage(OrderCancelledData _data, StanMsg msg)
        {
            var ticket = await _ticketRepo.GetByIdAsync(_data.ticket.id);

            if(ticket==null)
            {
                throw new Exception("Ticket Not Found");
            }

            ticket.orderId = null;
            await _ticketRepo.UpdateAsync(ticket.id, ticket);

            var evt = new TicketUpdatedEvent();
            evt.Data.price = ticket.price;
            evt.Data.title = ticket.title;
            evt.Data.userId = ticket.userId;
            evt.Data.version = ticket.version;
            evt.Data.id = ticket.id;

            await evt.Publish();

            msg.Ack();
        }
    }
}
