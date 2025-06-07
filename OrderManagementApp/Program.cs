//using Microsoft.EntityFrameworkCore;
using System.Collections;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;
using OrderManagementApp.Data;
using OrderManagementApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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
builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<UserService>();

var app = builder.Build();


AppUser.UserName = "anduenescu";
AppUser.Cart = new List<Product>();

app.UseHttpsRedirection();
app.UseAuthentication(); // added 
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello Andu!");

app.Run();
