using System.Security.Claims;
using System.Text;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using CarRentalApi.TokenGenrator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CarRentalApi.Repository;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication authentication;
        private readonly TokenService tokenService;
        private readonly EmailService emailService;
        private readonly IConfiguration configuration;

        public AuthenticationController(
            IAuthentication authentication,
            TokenService tokenService,
            EmailService emailService,
            IConfiguration configuration)
        {
            this.authentication = authentication;
            this.tokenService = tokenService;
            this.emailService = emailService;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var user = await authentication.GetUserByEmail(request.Email);
            if (user == null || string.IsNullOrEmpty(user.Email))
                return NotFound("User not found or email not set.");

            var resetToken = tokenService.GenerateToken(user.UserName); // Username goes into token claims
            user.ResetToken = resetToken;
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(5);
            await authentication.UpdateUser(user);

            var resetLink = $"https://localhost:7055/api/authentication/reset-password?token={resetToken}";
            var body = $"Click the link to reset your password (valid for 5 minutes): <a href='{resetLink}'>Reset Password</a>";

            await emailService.SendEmailAsync(user.Email, "Reset Your Password", body, true);

            return Ok("Password reset link sent to your email.");
        }

        // GET - HTML Reset Form
        [HttpGet("reset-password")]
        public IActionResult ResetPasswordForm([FromQuery] string token)
        {
            return Content($@"
                <html>
                    <head><title>Reset Password</title></head>
                    <body>
                        <h2>Reset Your Password</h2>
                        <form method='post' action='/api/authentication/reset-password'>
                            <input type='hidden' name='token' value='{token}' />
                            <label>New Password:</label><br/>
                            <input type='password' name='newPassword' required /><br/><br/>
                            <button type='submit'>Reset Password</button>
                        </form>
                    </body>
                </html>
            ", "text/html");
        }

        // POST - Reset Password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] string token, [FromForm] string newPassword)
        {
            var principal = tokenService.ValidateToken(token);
            if (principal == null)
                return BadRequest("Invalid or expired token.");

            var username = principal.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return BadRequest("Invalid token.");

            var user = await authentication.GetUserByUsername(username);
            if (user == null || user.ResetToken != token || user.ResetTokenExpiry < DateTime.UtcNow)
                return BadRequest("Invalid or expired token.");

            user.Password = newPassword;
            user.ResetToken = null;
            user.ResetTokenExpiry = null;
            await authentication.UpdateUser(user);

            return Content("<h3>Password has been reset successfully.</h3>", "text/html");
        }
    }

  
}
