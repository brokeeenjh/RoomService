using MediatR;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Query;

public record GetRoomQuery(Guid id) : IRequest<RoomDTO>;
public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, RoomDTO>
{
    private readonly IRoomService _roomService;
    
    public GetRoomQueryHandler(IRoomService roomService)
    {
        _roomService = roomService;
    }

    public async Task<RoomDTO> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var room = await _roomService.GetRoom(request.id);
        
        return room;
    }
}