using Microsoft.AspNetCore.Identity;

namespace RoomService.Domain.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public RoleEntity ? Role { get; set; }
    
    public Guid RoomId { get; set; }
    public RoomEntity RoomEntity {get; set;}
}