using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ticket.Common.MongoDb.V1
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = Environment.GetEnvironmentVariable("MONGO_URI");
                options.Database = Environment.GetEnvironmentVariable("MONGO_DB");
            });
        }
    }
}
