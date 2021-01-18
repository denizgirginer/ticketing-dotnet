using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;

namespace OrdersApi.Events
{
    public interface ITicketUpdatedListener : IListenerBase
    {
    }
}
