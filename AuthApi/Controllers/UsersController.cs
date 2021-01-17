using AuthApi.Models;
using AuthApi.Repo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Ticket.Common.Helpers;
using Ticket.Common.Utility;

namespace AuthApi.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserRepo _repo;
        public UsersController(IUserRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<User> SignUp([FromBody] User user)
        {

            var found = await _repo.GetAsync(x => x.email == user.email);

            if (found != null)
            {
                throw new Exception("Email in use");
            }

            var newUser = await _repo.AddAsync(new Models.User()
            {
                email = user.email,
                password = PasswordHash.HashPassword(user.password)
            }); ;

            return newUser;
        }

        [HttpPost]
        [Route("/signin")]
        public async Task<IActionResult> Signin([FromBody] User user)
        {
            var accessTokenResult = await _repo.SignInUser(user);

            //await HttpContext.SignInAsync(accessTokenResult.ClaimsPrincipal,
            //    accessTokenResult.AuthProperties);

            return Ok(new { success = true, token = accessTokenResult.AccessToken });
        }

        [HttpPost]
        [Route("/signout")]
        public IActionResult Signout()
        {
            
            return Ok(new { success = true });
        }

        [HttpGet]
        [Route("/currentuser")]
        [Authorize]
        public IActionResult CurrentUser()
        {
            
            return Ok(new { success = true, email = SessionHelper.GetUserEmail(), id=SessionHelper.GetUserId() });
        }

    }
}
