using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpirationSvc.JobQue
{
    public class OrderExpirationQue : JobQueBase<OrderJob>, IOrderExpirationQue
    {
        public override void Run(OrderJob job)
        {
            Console.Write(job.orderId+" order task finished **************");
        }
    }

    public class OrderJob
    {
        public string orderId { get; set; }
    }
}
