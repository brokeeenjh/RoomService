using Microsoft.EntityFrameworkCore;
using RoomService.Application.Interfaces.Repositories;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Dbcontext;

namespace RoomService.Infrastructure.Repositories;

public class BookRepository : IBookingRepository
{
    private readonly AppDbContext _dbContext;
    
    public BookRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }



    public async Task UpdateBook(Guid roomId, Guid userId, DateTime startDate, DateTime endDate)
    {
        var room = _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
        if (room == null)
            throw new Exception("Room not found");
        
        var user = _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new Exception("User not found");

        var bookingEntity = new BookingEntity()
        {
            RoomEntityId = roomId,
            UserEntityId = userId,
            StartDate = startDate,
            EndDate = endDate,
        };
        
        _dbContext.Bookings.Update(bookingEntity);
    }

    public async Task DeleteBook(Guid id)
    {
        var book = _dbContext.Bookings.FirstOrDefault(b => b.Id == id);
        if (book == null)
            throw new Exception("Book not found");
        
        _dbContext.Bookings.Remove(book);
       await _dbContext.SaveChangesAsync();
    }

    public async Task<BookingEntity> GetBook(Guid id)
    {
        var book = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
         
        if (book == null)
            throw new Exception("Book not found");
        
        return book;
    }

    public async Task<bool> IsOverlapped(Guid roomId, DateTime startTime , DateTime endTime)
    {
        return await _dbContext.Bookings
            .AnyAsync(b => b.Room.IsActive == true &&
                           b.RoomEntityId == roomId &&
                           b.StartDate < endTime &&
                           b.EndDate > startTime);
    }

    public async Task<BookingEntity> GetBookByUserId(Guid userId)
    {
        var booking = await _dbContext.Bookings
            .FirstOrDefaultAsync(x => x.UserEntityId == userId);
        
        if (booking == null)
            throw new Exception("Book not found");

        return booking;
    }

    public async Task<BookingEntity> GetBookByRoomId(Guid roomId)
    {
        var booking = await _dbContext.Bookings
            .FirstOrDefaultAsync(x => x.RoomEntityId == roomId);

        if (booking == null)
            throw new Exception("Room not found");

        return booking;
    }
}