using RoomService.Application.DTO_s;

namespace RoomService.Application.Interfaces.Services;

public interface IBookingService
{
    public Task CreateBookAsync(CreateBookingDTO booking);
    public Task<BookingDTO>  GetBookingAsync(Guid bookingId);
    public Task<BookingDTO> GetBookByRoomAsync(Guid roomId);
    public Task<BookingDTO> GetBookByUserIdAsync(Guid userId);
    public Task UpdateBookingAsync(UpdateBookingDTO booking);
    public Task DeleteBookingAsync(Guid bookingId);
    
}