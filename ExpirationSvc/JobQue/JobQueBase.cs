using Gofer.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpirationSvc.JobQue
{
    public class JobQueBase<T>  : IJobQueBase<T>
    {
        private TaskClient client;

        public async Task<bool> AddScheduledTask(Expression<Action> action, DateTime scheduledTime)
        {
            await client.TaskScheduler.AddScheduledTask(action, scheduledTime);

            return await Task.FromResult(true);
        }

        public async Task<bool> AddScheduledTask(T job, DateTime scheduledTime)
        {
            await AddScheduledTask(() => Run(job), scheduledTime);

            return await Task.FromResult(true);
        }

        public virtual void Run(T job)
        {
            Console.WriteLine("Task Finished");
            Console.WriteLine(this.GetType().FullName);
            Console.WriteLine(GetType().Name);
            Console.WriteLine("Task Finished");
        }
        

        public void Listen()
        {
            var REDIS_HOST = Environment.GetEnvironmentVariable("REDIS_HOST");
            var taskQueName = GetType().Name;

            Console.WriteLine(taskQueName);
            var que = TaskQueue.Redis(REDIS_HOST, taskQueName);

            var taskClient = new TaskClient(que);
            this.client = taskClient;

            taskClient.Listen();
        }
    }
}
