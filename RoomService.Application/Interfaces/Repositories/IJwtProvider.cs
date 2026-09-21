using RoomService.Domain.Entities;

namespace RoomService.Application.Interfaces.Repositories;

public interface IJwtProvider
{
    public string GenerateToken(UserEntity userEntity);
}