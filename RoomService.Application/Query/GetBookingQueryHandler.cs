using MediatR;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Query;

public record GetBookingQuery(Guid id) : IRequest<BookingDTO>;
public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingDTO>
{
    private readonly IBookingService _bookingService;

    public GetBookingQueryHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDTO> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookingService.GetBookingAsync(request.id);

        return book;
    }
}