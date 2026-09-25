using MediatR;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Command;

public record UpdateRoomCommand(Guid userId, Guid roomId) : IRequest<Task>;
public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Task>
{
    private readonly IRoomService  roomService;
    public UpdateRoomCommandHandler(IRoomService roomService)
    {
        this.roomService = roomService;
    }
    public  async Task<Task> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        await roomService.UpdateRoomAsync(request.roomId, request.userId);
        return Task.CompletedTask;
    }
}