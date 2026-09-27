using Microsoft.AspNetCore.Identity;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Dbcontext;

namespace RoomService.API.Seed;

public static class DbInitializator
{
    public static async Task Seed(AppDbContext dbContext, UserManager<UserEntity> userManager,
        RoleManager<RoleEntity> roleManager)
    {
        var role =  await roleManager.FindByNameAsync("User");

        if (role == null)
        {
            role = new RoleEntity()
            {
                Id = Guid.NewGuid(),
                Name = "User"
            };
            await roleManager.CreateAsync(role);
        }
        
    }
}