using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace OrdersApi.Repo
{
    public class TicketRepo : MongoDbRepositoryBase<Models.Ticket>, ITicketRepo
    {
        public override string CollectionName => "tickets";

        public TicketRepo(IOptions<MongoDbSettings> options) : base(options)
        {
        }

    }
}
