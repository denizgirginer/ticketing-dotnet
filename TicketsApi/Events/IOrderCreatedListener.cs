using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;

namespace TicketsApi.Events
{
    public interface IOrderCreatedListener:IListenerBase
    {
    }
}
