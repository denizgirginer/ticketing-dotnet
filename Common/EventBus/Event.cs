using System;
using System.Threading.Tasks;
using Ticket.Common.Events;

namespace Ticket.Common.EventBus
{
    public static class EventExtention
    {
        public static async Task Publish<T>(this Event<T> _event)
        {
            
            await NatsClient.Publish<T> (_event);
        }
    }

    public class EventBase
    {

    }

    abstract public class Event<T> :EventBase
    {
        abstract public Subjects Subject { get; }
        abstract public T Data { get;  }
    }

   

}
