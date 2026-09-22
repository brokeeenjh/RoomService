using RoomService.Domain.Entities;

namespace RoomService.Application.Interfaces.Repositories;

public interface IBookingRepository
{
    public Task UpdateBook(Guid roomId, Guid userId, DateTime startDate, DateTime endDate);
    public Task DeleteBook(Guid id);
    public Task<BookingEntity> GetBook(Guid id);
    public Task<bool> IsOverlapped(Guid roomId, DateTime endTime, DateTime startTime);
    public Task<BookingEntity> GetBookByUserId(Guid userId);
    public Task<BookingEntity> GetBookByRoomId(Guid roomId);
}