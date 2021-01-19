using ExpirationSvc.Events;
using ExpirationSvc.JobQue;
using Gofer.NET;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;
using Ticket.Common.Helpers;

namespace ExpirationSvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJwtForAPI();
            services.AddHttpContextAccessor();
            services.AddNats();

            services.AddSingleton<IOrderCreatedListener, OrderCreatedListener>();
            services.AddSingleton<IOrderExpirationQue, OrderExpirationQue>();

            /*
            var REDIS_HOST = Environment.GetEnvironmentVariable("REDIS_HOST");
            var que = TaskQueue.Redis(REDIS_HOST, "ExpirationService");

            var taskClient = new TaskClient(que);
            taskClient.TaskQueue.Enqueue(() => Console.WriteLine("Deneme task 2"));
             
            taskClient.TaskScheduler.AddScheduledTask(() => Console.WriteLine("Scheduled task 2"), DateTime.Now.AddSeconds(30));

            taskClient.Listen();*/

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor,
            IOrderCreatedListener orderCreatedListener,
            IOrderExpirationQue orderExpirationQue)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            orderCreatedListener.Subscribe();
            orderExpirationQue.Listen();


            //orderExpirationQue.AddScheduledTask(() => Console.WriteLine("Scheduled Task Queeee"), DateTime.Now.AddSeconds(30));
            /*
            orderExpirationQue.AddScheduledTask(new OrderJob()
            {
                orderId = "DENEMEORDER01"
            }, DateTime.Now.AddSeconds(10)); */

            app.UseHttpContext(httpContextAccessor);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
