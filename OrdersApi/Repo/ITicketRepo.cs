using System.Collections.Generic;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace OrdersApi.Repo
{
    public interface ITicketRepo : IRepository<Models.Ticket, string>
    {
        
    }
}
