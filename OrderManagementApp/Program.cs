//using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Repositories;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello Andu!");

app.Run();
