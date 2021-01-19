using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Common.EventBus;

namespace Ticket.Common.Events
{
    public class OrderCreatedEvent : Event<OrderCreatedData>
    {
        public override Subjects Subject => Subjects.OrderCreated;

        public override OrderCreatedData Data { get; set; } = new OrderCreatedData();
    }

    public class OrderCreatedData
    {
        public string id { get; set; }
        public OrderStatus status { get; set; }
        public string userId { get; set; }
        public DateTime expiresAt { get; set; }
        public int version { get; set; }
        public TicketData ticket { get; set; } = new TicketData();
    }

    public class TicketData
    {
        public string id { get; set; }
        public decimal price { get; set; }
    }
}
