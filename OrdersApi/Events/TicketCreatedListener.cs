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
    public class TicketCreatedListener : ListenerBase<TicketCreatedEvent, TicketCreatedData>, ITicketCreatedListener
    {
        public override string QueGroupName => "OrdersService";

        public override Subjects Subject => Subjects.TicketCreated;

        ITicketRepo _ticketRepo;
        public TicketCreatedListener(ITicketRepo ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }


        public override async void OnMessage(TicketCreatedData _data, StanMsg msg)
        {
            Console.WriteLine("Ticket created:" + _data.id);

            var ticket = new Models.Ticket();
            ticket.id = _data.id;
            ticket.title = _data.title;
            ticket.price = _data.price;
            //ticket.createdAt = _data.createdAt;
            //ticket.userId = _data.userId;
            ticket.version = _data.version;
            await _ticketRepo.AddAsync(ticket);

            msg.Ack();
        }
    }
}

