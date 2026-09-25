using RoomService.Application.DTO_s;

namespace RoomService.Application.Interfaces.Services;

public interface IRoomService
{
    public Task<RoomDTO>  GetRoom(Guid id);
    public Task<List<RoomDTO>> GetAllRooms();
    public Task<RoomDTO> GetUserRoom(Guid userId);
    public Task UpdateRoomAsync(Guid id, Guid userId);
    public Task<IEnumerable<RoomDTO>> GetAllFreeRooms();
    public Task DeleteRoomAsync(Guid id);
}