using RoomService.Application.Auth;
using RoomService.Application.Interfaces.Repositories;
using RoomService.Application.Interfaces.Services;
using RoomService.Domain;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Repositories;
using RoomService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookingRepository, BookRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService.Infrastructure.Services.RoomService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run((context) =>
{
    Console.WriteLine("Hello World!");
    return context.Response.WriteAsync("Hello World!");
});
app.Run();

