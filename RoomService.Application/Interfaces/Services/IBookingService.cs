using RoomService.Application.DTO_s;

namespace RoomService.Application.Interfaces.Services;

public interface IBookingService
{
    public Task CreateBookAsync(CreateBookingDTO booking);
    public Task<BookingDTO>  GetBookingAsync(Guid bookingId);
    public Task UpdateBookingAsync(UpdateBookingDTO booking);
    public Task DeleteBookingAsync(Guid bookingId);
    
}