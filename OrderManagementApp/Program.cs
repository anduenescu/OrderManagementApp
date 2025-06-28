//using Microsoft.EntityFrameworkCore;
using System.Collections;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;
using OrderManagementApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.ASCII.GetBytes("this_is_a_very_secure_super_secret_key_123!");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSingleton<CategoryRepository>();// a step
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<OrderService>();

var app = builder.Build();


AppUser.UserName = "anduenescu";
AppUser.Cart = new List<Product>();

app.UseHttpsRedirection();
app.UseAuthentication(); // added 
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello Andu!");

/*string HashPassword(string password, byte[] salt)
{
    using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
    return Convert.ToBase64String(pbkdf2.GetBytes(32));
}

app.MapGet("/", () =>
{
    string hashed = HashPassword("adminpassword", Encoding.UTF8.GetBytes("admin"));
    return $"Hello Andu!<br><br>Admin password hash:<br><code>{hashed}</code>";
}); */

app.Run();
