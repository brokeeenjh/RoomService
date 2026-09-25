using RoomService.Domain.Entities;

namespace RoomService.Application.Interfaces.Repositories;

public interface IRoomRepository
{
    public Task<IEnumerable<RoomEntity>> GetAllRoomsAsync();
    public Task<RoomEntity> GetRoomByIdAsync(Guid id);
    public Task<RoomEntity> GetRoomByUserIdAsync(Guid id);
    public Task<IEnumerable<RoomEntity>> FreeRoomsAsync();
    public Task DeleteRoomAsync(Guid id);

    public Task UpdateRoomAsync(Guid id, Guid userId);
}