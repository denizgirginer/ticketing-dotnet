using JwtGenerator.ServiceCollection.Extensions.JwtBearer;
using JwtGenerator.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Ticket.Common.EventBus;
using Ticket.Common.Helpers;
using Ticket.Common.MongoDb.V1;
using TicketsApi.Events;
using TicketsApi.Repo;

namespace TicketsApi
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
            services.AddSingleton<IOrderCancelledListener, OrderCancelledListener>();


            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiresAdmin", policy => policy.RequireClaim("HasAdminRights"));

            });

            services.AddControllers();

            services.AddMongoDbSettings(Configuration);
            services.AddSingleton<ITicketRepo, TicketRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor,
            IOrderCancelledListener orderCancelledListener,
            IOrderCreatedListener orderCreatedListener)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            orderCreatedListener.Subscribe();
            orderCancelledListener.Subscribe();


            app.UseHttpContext(httpContextAccessor);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
