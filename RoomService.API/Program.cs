using Microsoft.AspNetCore.Identity;
using RoomService.API.Seed;
using RoomService.Application.Auth;
using RoomService.Application.Interfaces.Repositories;
using RoomService.Application.Interfaces.Services;
using RoomService.Domain;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Dbcontext;
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

using (var scope = app.Services.CreateScope())
{
    try
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<AppDbContext>();
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<RoleEntity>>();

        await DbInitializator.Seed(dbContext, userManager, roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        throw ex;
    }
    
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

