using AuthApi.Repo;
using JwtGenerator.ServiceCollection.Extensions.JwtBearer;
using JwtGenerator.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Ticket.Common.MongoDb.V1;

namespace AuthApi
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
            var tokenOptions = new TokenOptions(
                Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Environment.GetEnvironmentVariable("JWT_KEY"), 30);

            services.AddJwtAuthenticationForAPI(tokenOptions); 

            //services.AddJwtAuthenticationWithProtectedCookie(tokenOptions, "Ticketing");
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiresAdmin", policy => policy.RequireClaim("HasAdminRights"));
            });

            services.AddControllers();

            services.AddMongoDbSettings(Configuration);
            services.AddScoped<IUserRepo, UserRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
