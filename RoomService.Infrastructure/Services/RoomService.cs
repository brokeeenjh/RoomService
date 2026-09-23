using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Repositories;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Infrastructure.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    
    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<RoomDTO> GetRoom(Guid id)
    {
        var room = await _roomRepository.GetRoomByIdAsync(id);
        
        if (room == null)
            throw new Exception("this room doesn't exist");

        var roomToDTO = new RoomDTO()
        {
            Id =  room.Id,
            bookId = room.bookId,
            userId = room.UserEntityId
        };

        return roomToDTO;
    }

    public async Task<List<RoomDTO>> GetAllRooms()
    {
        var rooms = await _roomRepository.GetAllRoomsAsync();
        
        if (rooms == null)
            throw new Exception("this room doesn't exist");

        var roomsToDTO = rooms.Select(x => new RoomDTO()
        {
            Id = x.Id,
            bookId = x.bookId,
            userId = x.UserEntityId
        });
        
        return roomsToDTO.ToList();
    }

    public async Task<RoomDTO> GetUserRoom(Guid userId)
    {
        var room = await _roomRepository.GetRoomByUserIdAsync(userId);
        
        if (room == null)
            throw new Exception("this room doesn't exist");
        var roomToDTO = new RoomDTO()
        {
            Id = room.Id,
            bookId = room.bookId,
            userId = room.UserEntityId
        };
        
        return roomToDTO;
    }

   
}