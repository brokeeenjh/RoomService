using Microsoft.AspNetCore.Identity;

namespace RoomService.Domain.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public RoleEntity ? Role { get; set; }

    public BookingEntity bookingEntity { get; set; }
}