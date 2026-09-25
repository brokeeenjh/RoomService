using MediatR;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Command;

public record UpdateBookingCommand(Guid bookingId, Guid roomId, DateTime startDate, DateTime endDate) : IRequest<Task>;
public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Task>
{
    private readonly IBookingService  _bookingService;

    public UpdateBookingCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<Task> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        await _bookingService.UpdateBookingAsync(new UpdateBookingDTO()
        {
            bookingId = request.bookingId,
            roomId = request.roomId,
            startDate = request.startDate,
            endDate = request.endDate
        });
        
        return Task.CompletedTask;
    }
}