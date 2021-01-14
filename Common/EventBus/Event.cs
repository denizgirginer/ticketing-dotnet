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

    public class Event<T> :EventBase
    {
        public Subjects Subject { get; set; }
        public T Data { get; set; }
    }

    public class TestEvent: Event<TestData>
    {
        
    }

    public class TestData
    {
        public string message { get; set; }
    }

    public class TestPublisher
    {
        public async void TestEvent()
        {
            await new TestEvent() { 
                Data = new TestData()
                {

                }
            }.Publish();
        }
    }

}
