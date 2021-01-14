using System.Collections.Generic;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace TicketsApi.Repo
{
    public interface ITicketRepo : IRepository<Models.Ticket, string>
    {
        Task<List<Models.TicketBase>> GetTickets();
        Task<Models.TicketBase> GetTicketById(string id);
    }
}
