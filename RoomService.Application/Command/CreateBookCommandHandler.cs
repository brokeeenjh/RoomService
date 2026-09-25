using MediatR;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Command;

public record CreateBookCommand(Guid roomId, Guid userId, string email, DateTime startDate, DateTime endDate ) : IRequest<Task>;
public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Task>
{
    private readonly IBookingService  _bookingService;

    public CreateBookCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    public async Task<Task> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        await _bookingService.CreateBookAsync(new CreateBookingDTO()
        {
            roomId = request.roomId,
            userId = request.userId,
            Email = request.email,
            startDate = request.startDate,
            endDate = request.endDate
        });
        return Task.CompletedTask;
    }
}