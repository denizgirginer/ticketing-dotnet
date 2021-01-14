using AuthApi.Models;
using AuthApi.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        IUserRepo _repo;
        public TestController(IUserRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public string Get()
        {
            return "Date v7:" + DateTime.Now.ToString()+TestPackage.Hello2("Deniz");
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            return Ok(_repo.Get().ToList());
        }

        [HttpPost]
        [Route("users")]
        public IActionResult Create([FromBody] User data)
        {
            var result = _repo.AddAsync(data).Result;
            return Ok(result);
        }
    }
}
