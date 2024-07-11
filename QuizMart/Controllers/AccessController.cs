using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizMart.Models.ViewModels;
using QuizMart.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizMart.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [AllowAnonymous]
    public class AccessController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccessService _accessService;

        public AccessController(IConfiguration config, IAccessService accessService)
        {
            _config = config;
            _accessService = accessService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            IActionResult response = Unauthorized();
            var user = await _accessService.Login(model);

                if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user, model.RememberMe);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        // Helper
        [NonAction]
        private string GenerateJSONWebToken(UserInfo user, bool rememberMe)
        {
            if (user != null)
            {
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role!)
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Define different expiration times based on the 'rememberMe' parameter.
                DateTime? expirationTime = rememberMe
                    ? DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:ExpiresInRememberMe"]!))  // Longer expiration for "remember me"
                    : DateTime.UtcNow.AddHours(Convert.ToDouble(_config["Jwt:ExpiresIn"]!));   // Shorter expiration for regular sessions

                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: expirationTime,
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                return "Invalid credentials";
            }
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel model)
        {
            if (model.Password != model.ConfirmPassword) return BadRequest("Password and confirm password must be the same");

            IActionResult response = Unauthorized();

            var success = await _accessService.Signup(model);

            if (success)
            {
                response = Ok("Signup successful");
            }
            else
            {
                response = BadRequest("Signup failed. Please check your input.");
            }

            return response;
        }
    }
}
