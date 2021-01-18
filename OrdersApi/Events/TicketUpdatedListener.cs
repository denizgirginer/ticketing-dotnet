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
    public class TicketUpdatedListener : ListenerBase<TicketUpdatedEvent, TicketUpdatedData>, ITicketUpdatedListener
    {
        public override string QueGroupName => "OrdersService";

        public override Subjects Subject => Subjects.TicketUpdated;

        ITicketRepo _ticketRepo;
        public TicketUpdatedListener(ITicketRepo ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public override async void OnMessage(TicketUpdatedData _data, StanMsg msg)
        {
            Console.WriteLine($"Ticked Updating:{_data.id} Version:{_data.version} ");

            var found = await _ticketRepo.GetAsync(x => x.id == _data.id && x.version == _data.version - 1);

            if(found==null)
            {
                Console.WriteLine($"Ticked Not Found:{_data.id} Version:{_data.version-1} ");
                msg.Ack();
                throw new Exception("Not Found");               
            }

            found.title = _data.title;
            found.price = _data.price;
            found.version = _data.version;

            await _ticketRepo.UpdateAsync(found.id, found);

            msg.Ack();

        }
    }
}
