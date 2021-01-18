using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Common.EventBus;

namespace Ticket.Common.Events
{
    public class TicketCreatedEvent : Event<TicketCreatedData>
    {
        public override Subjects Subject => Subjects.TicketCreated;
        public override TicketCreatedData Data { get; set; } = new TicketCreatedData();

    }

    public class TicketCreatedData
    {
        public string id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string userId { get; set; }
        public int version { get; set; }
        public string orderId { get; set; }
    }
}
