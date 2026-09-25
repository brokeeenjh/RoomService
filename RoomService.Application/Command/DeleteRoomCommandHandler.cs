using MediatR;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Command;

public record DeleteRoomCommand(Guid RoomId) : IRequest<bool>;
public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, bool>
{
    private readonly IRoomService  _roomService;
    public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        await _roomService.DeleteRoomAsync(request.RoomId);
        return true;
    }
}