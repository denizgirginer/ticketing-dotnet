using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Common.EventBus;

namespace Ticket.Common.Events
{
    public class ExpirationCompleteEvent : Event<ExpirationCompleteData>
    {
        public override Subjects Subject => Subjects.ExpirationComplete;

        public override ExpirationCompleteData Data => new ExpirationCompleteData();
    }

    public class ExpirationCompleteData
    {
        public string orderId { get; set; }
    }
}
