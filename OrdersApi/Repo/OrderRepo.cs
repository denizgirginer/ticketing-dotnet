using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.Events;
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
                x.status==OrderStatus.Created || 
                x.status==OrderStatus.AwaitingPayment ||
                x.status==OrderStatus.Complete
            ));
            return found!=null;
        }

        public async Task<Models.Order> GetOrderById(string orderId)
        {
            var order = await GetByIdAsync(orderId);
            order.ticket = await _ticketRepo.GetByIdAsync(order.ticketId);

            return await Task.FromResult(order);
        }

        public async Task<List<Models.Order>> GetUserOrders(string userId)
        {
            List<Models.Order> orders = null;

            await Task.Run(() =>
            {
                orders = Get(x => x.userId == userId).ToList(); 
            });

            foreach(var order in orders)
            {
                order.ticket = await _ticketRepo.GetByIdAsync(order.ticketId);
            }

            return await Task.FromResult(orders);
        }
    }
}
