using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        //[Authorize]
        [Route("/")]
        public async Task<Models.TicketBase> NewTicket(Models.TicketBase ticket)
        {
            var newTicket = new Models.Ticket()
            {
                title=ticket.title, 
                price=ticket.price,
                userId=GetUserID()
            };
            await _repo.AddAsync(newTicket);

            //TODO publish ticket
            Console.WriteLine("Ticket added");

            return await _repo.GetTicketById(newTicket.Id); 
        }


        [HttpPut]
        //[Authorize]
        [Route("/")]
        public async Task<IActionResult> UpdateTicket(Models.TicketBase ticket)
        {
            var _ticket = await _repo.GetByIdAsync(ticket.Id);

            if(_ticket==null)
            {
                throw new Exception("Not Found");
            }

            _ticket.title = ticket.title;
            _ticket.price = ticket.price;
            
            await _repo.UpdateAsync(_ticket.Id, _ticket);

            //TODO publish ticket

            return Ok(_ticket as Models.TicketBase);
        }




        private string GetUserID()
        {
            var claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "userId");

            return claim?.Value;
        }
    }
}
