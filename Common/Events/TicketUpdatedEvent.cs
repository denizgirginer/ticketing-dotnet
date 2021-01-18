using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Common.EventBus;

namespace Ticket.Common.Events
{
    public class TicketUpdatedEvent : Event<TicketUpdatedData>
    {
        public override Subjects Subject => Subjects.TicketUpdated;
        public override TicketUpdatedData Data => new TicketUpdatedData(); 
    }

    public class TicketUpdatedData
    {        
        public string id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string userId { get; set; }
        public int version { get; set; }
        public string orderId { get; set; }
    }
}
