using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Common.EventBus;

namespace Ticket.Common.Events
{
    public class PaymentCreatedEvent : Event<PaymentCreatedData>
    {
        public override Subjects Subject => Subjects.PaymentCreated;

        public override PaymentCreatedData Data { get; set; } = new PaymentCreatedData();
    }

    public class PaymentCreatedData
    {
        public string orderId { get; set; }
        public string stripeId { get; set; }
    }
}
