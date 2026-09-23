using RoomService.Domain.Entities;

namespace RoomService.Application.Interfaces.Repositories;

public interface IBookingRepository
{
    public Task UpdateBook(Guid bookingEntityId, Guid roomId, Guid userId, DateTime startDate, DateTime endDate);
    public Task DeleteBook(Guid id);
    public Task<BookingEntity> GetBook(Guid id);
    public Task<bool> IsOverlapped(Guid roomId, DateTime startTime, DateTime EndTime, Guid? excludeBookingId = null);
    public Task<BookingEntity> GetBookByUserId(Guid userId);
    public Task<BookingEntity> GetBookByRoomId(Guid roomId);
    public Task CreateBook(BookingEntity booking);
}