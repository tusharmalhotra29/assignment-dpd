using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Project1.Models;
using Project1.Models.DTO;
using Project1.Security.Services;
using SQLitePCL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project1.Security.Token
{

   

    [ApiController]
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthenticationServices _authenticationService;

        public TokenController(IConfiguration configuration, AuthenticationServices authenticationService)
        {
            _configuration = configuration;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult GenerateToken(UserCredentials credentials)
        {
            try
            {
                var user = _authenticationService.Authenticate(credentials.Username, credentials.Password);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid credentials." });
                }

                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                // Add any additional claims you want to include in the token.
            }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
