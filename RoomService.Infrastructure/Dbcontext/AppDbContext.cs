using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoomService.Domain.Entities;

namespace RoomService.Infrastructure.Dbcontext;

public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    public DbSet<RoomEntity>  Rooms { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
    {
        
    }
}