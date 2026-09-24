using MediatR;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Query;

public record GetBookByUserQuery(Guid id) : IRequest<BookingDTO>;
public class GetBookByUserQueryHandler : IRequestHandler<GetBookByUserQuery, BookingDTO>
{
    private readonly IBookingService _bookingService;

    public GetBookByUserQueryHandler(
        IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDTO> Handle(GetBookByUserQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookingService.GetBookByUserIdAsync(request.id);
        
        return book;
    }
}