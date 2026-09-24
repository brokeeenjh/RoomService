using MediatR;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Query;

public record GetBookByRoomQuery(Guid RoomId) : IRequest<BookingDTO>;
public class GetBookByRoomQueryHandler : IRequestHandler<GetBookByRoomQuery, BookingDTO>
{
    private readonly IBookingService  _bookingService;

    public GetBookByRoomQueryHandler(
        IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDTO> Handle(GetBookByRoomQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookingService.GetBookByRoomAsync(request.RoomId);
        
        return book;
    }
}