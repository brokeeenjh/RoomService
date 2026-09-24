using MediatR;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Query;

public record GetAllRoomsQuery() : IRequest<IEnumerable<RoomDTO>>;
public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, IEnumerable<RoomDTO>>
{
    private readonly IRoomService  _roomService;
    
    public GetAllRoomsQueryHandler(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    public async Task<IEnumerable<RoomDTO>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomService.GetAllRooms();

        return rooms;
    }
}