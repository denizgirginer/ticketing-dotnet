using Microsoft.AspNetCore.Mvc;
using System;

namespace dotnet_demo_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        
        public TestController()
        {
            
        }

        [HttpGet]
        public string Get()
        {
            return "Date:"+DateTime.Now.ToString();
        }
    }
}
