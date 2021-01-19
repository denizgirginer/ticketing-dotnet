using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Models.Request;
using OrdersApi.Repo;
using System;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;
using Ticket.Common.Events;
using Ticket.Common.Helpers;
using Ticket.Common.Middleware;

namespace OrdersApi.Controllers
{
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        ITicketRepo _ticketRepo;
        IOrderRepo _orderRepo;
        public OrdersController(ITicketRepo ticketRepo, IOrderRepo orderRepo)
        {
            _ticketRepo = ticketRepo;
            _orderRepo = orderRepo;
        }

        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> GetOrders()
        {
            var userId = SessionHelper.GetUserId();

            var orders = await _orderRepo.GetUserOrders(userId);

            return Ok(orders);
        }

        [HttpGet]
        [Route("/{orderId}")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound);
            }

            if (order.userId != SessionHelper.GetUserId())
            {
                throw new CustomException(System.Net.HttpStatusCode.Unauthorized);
            }

            order.ticket = await _ticketRepo.GetByIdAsync(order.ticketId);

            return Ok(order);
        }

        [HttpDelete]
        [Route("/{orderId}")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound);
            }

            if (order.userId != SessionHelper.GetUserId())
            {
                throw new CustomException(System.Net.HttpStatusCode.Unauthorized);
            }

            await _orderRepo.DeleteAsync(order);

            //publish OrderCancelled
            var evt = new OrderCancelledEvent();
            evt.Data.id = order.id;
            evt.Data.ticket.id = order.ticket.id;
            await evt.Publish();

            return Ok(true);
        }

        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> NewOrder(OrderModel ticket)
        {
            var found = await _ticketRepo.GetByIdAsync(ticket.ticketId);

            if (found == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound);
            }

            if (await _orderRepo.IsReserved(ticket.ticketId))
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest);
            }

            var expirationDate = DateTime.Now.AddMinutes(2); 

            var newOrder = new Models.Order()
            {
                status = OrderStatus.Created,
                ticket = found,
                ticketId = ticket.ticketId,
                expiresAt = expirationDate,
                userId = SessionHelper.GetUserId()
            };

            await _orderRepo.AddAsync(newOrder);

            //publish event OrderCreated
            var evt = new OrderCreatedEvent();
            evt.Data.expiresAt = newOrder.expiresAt;
            evt.Data.id = newOrder.id;
            evt.Data.status = newOrder.status;
            evt.Data.userId = newOrder.userId;
            evt.Data.version = newOrder.version;
            evt.Data.ticket.id = newOrder.ticket.id;
            evt.Data.ticket.price = newOrder.ticket.price;
            await evt.Publish();

            return Ok(newOrder);
        }

    }
}
