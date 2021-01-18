using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Common.Events
{
    public enum Subjects
    {
        TicketCreated,
        TicketUpdated,

        OrderCreated,
        OrderCancelled,
        OrderUpdated,

        ExpirationComplete,

        PaymentCreated
    }
}
