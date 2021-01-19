using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Common.Events
{
    public enum OrderStatus
    {
        Created,
        Cancelled,
        AwaitingPayment,
        Complete
    }
}
