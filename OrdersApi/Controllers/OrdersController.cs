using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Models.Request;
using OrdersApi.Repo;
using System;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.Helpers;

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

            if(order==null)
            {
                throw new Exception("Not Found");
            }

            if(order.userId!=SessionHelper.GetUserId())
            {
                throw new Exception("Not Authoriez");
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
                throw new Exception("Not Found");
            }

            if (order.userId != SessionHelper.GetUserId())
            {
                throw new Exception("Not Authoriez");
            }

            await _orderRepo.DeleteAsync(order);

            //TODO publish event OrderCancelled

            return Ok(true);
        }

        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> NewOrder(OrderModel ticket)
        {
            var found = await _ticketRepo.GetByIdAsync(ticket.ticketId);

            if(found==null)
            {
                throw new Exception("Not Found Ticket");
            }

            if(await _orderRepo.IsReserved(ticket.ticketId))
            {
                throw new Exception("Ticket already reserved");
            }

            var expirationDate = DateTime.Now.AddMinutes(30);

            var newOrder = new Models.Order() { 
                status=Models.OrderStatus.Created,
                ticket=found,
                ticketId = ticket.ticketId,
                expiresAt = expirationDate,
                userId = SessionHelper.GetUserId()
            };

            await _orderRepo.AddAsync(newOrder);

            //TODO publish event OrderCreated

            return Ok(newOrder);
        }

    }
}
