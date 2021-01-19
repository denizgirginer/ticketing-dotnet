using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace OrdersApi.Repo
{
    public interface IOrderRepo : IRepository<Models.Order, string>
    {
        Task<bool> IsReserved(string ticketId);
        Task<List<Models.Order>> GetUserOrders(string userId);
        Task<Models.Order> GetOrderById(string orderId);
    }
}
