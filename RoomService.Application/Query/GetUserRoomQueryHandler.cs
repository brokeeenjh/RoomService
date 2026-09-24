using MediatR;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Query;

public record GetUserRoomQuery(Guid id) : IRequest<RoomDTO>;
public class GetUserRoomQueryHandler : IRequestHandler<GetUserRoomQuery, RoomDTO>
{
    private readonly IRoomService  _roomService;
    
    public GetUserRoomQueryHandler(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    public async Task<RoomDTO> Handle(GetUserRoomQuery request, CancellationToken cancellationToken)
    {
        var room = await _roomService.GetUserRoom(request.id);

        return room;
    }
}