using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpirationSvc.JobQue
{
    public interface IOrderExpirationQue : IJobQueBase<OrderJob>
    {
    }   

}
