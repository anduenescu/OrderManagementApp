//using Microsoft.EntityFrameworkCore;
using System.Collections;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<CategoryRepository>();// a step
builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<OrderRepository>();
var app = builder.Build();


AppUser.UserName = "anduenescu";
AppUser.Cart = new List<Product>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello Andu!");

app.Run();
