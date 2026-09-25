using MediatR;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Command;

public record DeleteBookCommand(Guid BookID) : IRequest<bool>;
public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookingService  _bookingService;

    public DeleteBookCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        await _bookingService.DeleteBookingAsync(request.BookID);
        
        return true;
    }
}