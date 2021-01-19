using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpirationSvc.JobQue
{
    public interface IJobQueBase<T>
    {
        Task<bool> AddScheduledTask(Expression<Action> action, DateTime scheduledTime);
        Task<bool> AddScheduledTask(T job, DateTime scheduledTime);
        void Listen();
    }

}
