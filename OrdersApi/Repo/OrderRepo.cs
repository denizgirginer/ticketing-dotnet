using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace OrdersApi.Repo
{
    public class OrderRepo : MongoDbRepositoryBase<Models.Order>, IOrderRepo
    {
        public override string CollectionName => "orders";

        ITicketRepo _ticketRepo;
        public OrderRepo(IOptions<MongoDbSettings> options, ITicketRepo ticketRepo) : base(options)
        {
            _ticketRepo = ticketRepo;
        }


        public async Task<bool> IsReserved(string ticketId)
        {
            var found = await this.GetAsync(x => x.ticketId == ticketId && (
                x.status==Models.OrderStatus.Created || 
                x.status==Models.OrderStatus.AwaitingPayment ||
                x.status==Models.OrderStatus.Complete
            ));
            return found!=null;
        }

        public async Task<List<Models.Order>> GetUserOrders(string userId)
        {
            List<Models.Order> orders = null;

            await Task.Run(() =>
            {
                orders = Get(x => x.userId == userId).ToList();

                orders.ForEach(async order =>
                {
                    order.ticket = await _ticketRepo.GetByIdAsync(order.ticketId);
                });
            });

            return await Task.FromResult(orders);
        }
    }
}
