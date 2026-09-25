using MediatR;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Query;

public record GetFreeRoomsQuery() : IRequest<IEnumerable<RoomDTO>>;
public class GetFreeRoomsQueryHandler : IRequestHandler<GetFreeRoomsQuery, IEnumerable<RoomDTO>>
{
    private readonly IRoomService _roomService;

    public GetFreeRoomsQueryHandler(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    public async Task<IEnumerable<RoomDTO>> Handle(GetFreeRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomService.GetAllFreeRooms();
        
        return rooms;
    }
}