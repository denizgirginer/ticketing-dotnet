using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace TicketsApi.Repo
{
    public class TicketRepo : MongoDbRepositoryBase<Models.Ticket>, ITicketRepo
    {
        public override string CollectionName => "tickets";

        public TicketRepo(IOptions<MongoDbSettings> options) : base(options)
        {
        }

        public async Task<List<Models.TicketBase>> GetTickets()
        {
            List<Models.TicketBase> tickets=null;

            await Task.Run(() =>
            {
                tickets = Get(x => x.orderId == null).Select(x => x as Models.TicketBase).ToList();
            });

            return await Task.FromResult(tickets);
        }

        public async Task<Models.TicketBase> GetTicketById(string id)
        {
            Models.TicketBase ticket = null;

            await Task.Run(() =>
            {
                ticket = Get(x => x.Id == id).Select(x => x as Models.TicketBase).FirstOrDefault();
            });

            return await Task.FromResult(ticket);
        }

    }
}
