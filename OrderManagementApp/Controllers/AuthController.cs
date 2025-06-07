using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OrderManagementApp.Models;
using OrderManagementApp.Services;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    UserService UserService;

    public AuthController(UserService userService) 
    {
        this.UserService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // 🔐 Replace this with a real DB lookup // here to check the password in the DB // no need to check username but only the password 
        
        if (UserService.Login(request.Username, request.Password) != null)
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this_is_a_very_secure_super_secret_key_123!"); // use config in production

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, request.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

        return Unauthorized("Invalid credentials");
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
