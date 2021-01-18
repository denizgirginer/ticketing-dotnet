using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Common.EventBus;

namespace Ticket.Common.Events
{
    public class OrderCancelledEvent : Event<OrderCancelledData>
    {
        public override Subjects Subject => Subjects.OrderCancelled;

        public override OrderCancelledData Data => new OrderCancelledData();
    }

    public class OrderCancelledData
    {
        public string id { get; set; }
        public int version { get; set; }
        public TicketDataCancelled ticket => new TicketDataCancelled();
    }

    public class TicketDataCancelled {
        public string id { get; set; }
    }

}
