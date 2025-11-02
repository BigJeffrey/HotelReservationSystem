using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Services;
using HotelReservationSystem.Persistence;
using HotelReservationSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context Configuration
var connectionString = Environment.GetEnvironmentVariable("HOTEL_DB_CONNECTION");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Environment variable 'HOTEL_DB_CONNECTION' is not set.");
}
builder.Services.AddDbContext<HotelDbContext>(options => options.UseNpgsql(connectionString));

// Dependency Injection for Application Services and Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
