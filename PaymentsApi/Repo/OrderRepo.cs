using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace PaymentsApi.Repo
{
    public class OrderRepo : MongoDbRepositoryBase<Models.Order>, IOrderRepo
    {
        public override string CollectionName => "orders";

        public OrderRepo(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
