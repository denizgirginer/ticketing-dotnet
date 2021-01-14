using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace PaymentsApi.Repo
{
    public interface IOrderRepo : IRepository<Models.Order, string>
    {
        
    }
}
