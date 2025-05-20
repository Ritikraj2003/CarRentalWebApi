using CarRentalApi.Interface;
using CarRentalApi.Models;
using CarRentalApi.TokenGenrator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication authentication;
        private readonly TokenService tokenService;

        public AuthenticationController(IAuthentication authentication, TokenService tokenService)
        {
            this.authentication = authentication;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task <IActionResult> Login([FromBody] User user)
        {
            var validUser = authentication.Login(user.UserName, user.Password);
            if (validUser == null)
                return Unauthorized("Invalid credentials");

            var token = tokenService.GenerateToken(user.UserName);
            return Ok(new { token });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var res = await authentication.Add(user);
            return Ok(res);
        }
    }
}