using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;
using Ticket.Common.Events;
using Ticket.Common.Helpers;
using TicketsApi.Repo;

namespace TicketsApi.Controllers
{
    [ApiController]
    public class TicketsController : ControllerBase
    {
        ITicketRepo _repo;
        public TicketsController(ITicketRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _repo.GetTickets();

            return Ok(tickets);
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> GetTicket(string id)
        {
            var ticket = await _repo.GetTicketById(id);

            return Ok(ticket);
        }

        [HttpDelete]
        [Authorize("RequiresAdmin")]
        [Route("/{ticketId}")]
        public async Task<IActionResult> DeleteTicket(string ticketId)
        {
            var ticket = await _repo.GetByIdAsync(ticketId);

            await _repo.DeleteAsync(ticket);

            return  await Task.FromResult(Ok(new { success=true }));
        }

        [HttpPost]
        [Authorize]
        [Route("/")]
        public async Task<Models.TicketBase> NewTicket(Models.TicketBase ticket)
        {
            var newTicket = new Models.Ticket()
            {
                title=ticket.title, 
                price=ticket.price,
                userId=SessionHelper.GetUserId()
            };
            await _repo.AddAsync(newTicket);

            //publish ticket
            Console.WriteLine("Ticket added");
            var evt = new TicketCreatedEvent();
            evt.Data.id = newTicket.id;
            evt.Data.title = newTicket.title;
            evt.Data.price = newTicket.price;
            evt.Data.userId = newTicket.userId;
            evt.Data.version = newTicket.version;
            await evt.Publish();

            return await _repo.GetTicketById(newTicket.id); 
        }


        [HttpPut]
        [Authorize]
        [Route("/{ticketId}")]
        public async Task<IActionResult> UpdateTicket(Models.TicketBase ticket)
        {
            var _ticket = await _repo.GetByIdAsync(ticket.id);

            if(_ticket==null)
            {
                throw new Exception("Not Found");
            }

            _ticket.title = ticket.title;
            _ticket.price = ticket.price;

            Console.WriteLine("Update Ticket Version:" + _ticket.version);

            await _repo.UpdateAsync(_ticket.id, _ticket);

            Console.WriteLine("Update Ticket Version:" + _ticket.version);

            //publish ticket
            var evt = new TicketUpdatedEvent();
            evt.Data.id = _ticket.id;
            evt.Data.title = _ticket.title;
            evt.Data.price = _ticket.price;
            evt.Data.userId = _ticket.userId;
            evt.Data.version = _ticket.version;
            Console.WriteLine("Event Version:"+ _ticket.version);
            await evt.Publish();

            return Ok(_ticket as Models.TicketBase);
        }


    }
}
