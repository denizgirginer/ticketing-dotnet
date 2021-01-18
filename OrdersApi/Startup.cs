using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersApi.Events;
using OrdersApi.Repo;
using Ticket.Common.EventBus;
using Ticket.Common.Helpers;
using Ticket.Common.MongoDb.V1;

namespace OrdersApi
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

            services.AddSingleton<ITicketCreatedListener, TicketCreatedListener>();
            services.AddSingleton<ITicketUpdatedListener, TicketUpdatedListener>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiresAdmin", policy => policy.RequireClaim("HasAdminRights"));
            });

            services.AddMongoDbSettings(Configuration);
            services.AddSingleton<ITicketRepo, TicketRepo>();
            services.AddSingleton<IOrderRepo, OrderRepo>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, 
            ITicketCreatedListener ticketCreatedListener,
            ITicketUpdatedListener ticketUpdatedListener)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ticketCreatedListener.Subscribe();
            ticketUpdatedListener.Subscribe();


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
