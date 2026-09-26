using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoomService.Application.Interfaces.Repositories;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Dbcontext;

namespace RoomService.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext  _dbContext;
    public RoomRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<RoomEntity>> GetAllRoomsAsync()
    {
        return await _dbContext.Rooms.ToListAsync();
    }

    public async Task<RoomEntity> GetRoomByIdAsync(Guid id)
    {
        var room = await _dbContext.Rooms
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (room == null)
            throw new Exception("Room not found");
        
        return room;
    }

    public async Task<RoomEntity> GetRoomByUserIdAsync(Guid id)
    {
        var user = await _dbContext.Users
            .Include(x => x.bookingEntity)
            .ThenInclude(x => x.Room)
            .FirstOrDefaultAsync( x => x.Id == id );

        if (user == null)
        {
            throw new Exception("User not found");
        }   
        
        if (user.bookingEntity == null)
            throw new Exception("User is not booking");
        
        return user.bookingEntity.Room;
    }

    public async Task<IEnumerable<RoomEntity>> FreeRoomsAsync()
    {
       var rooms =  await _dbContext.Rooms
           .Include(x => x.BookingEntity)
           .Where(x => x.IsActive == true)
           .Where(x => x.BookingEntity.StartDate >  DateTime.UtcNow ||
                       x.BookingEntity.EndDate < DateTime.UtcNow ||
                       x.BookingEntity == null)
           .OrderBy( x=>x.BookingEntity.StartDate)
            .ToListAsync();
       return rooms;
    }

    public async Task DeleteRoomAsync(Guid id)
    {
        var room = await _dbContext.Rooms
            .FirstOrDefaultAsync(x => x.Id == id);
        
        _dbContext.Rooms.Remove(room);
        await _dbContext.SaveChangesAsync();
    }
    

    public async Task UpdateRoomAsync(Guid roomId, Guid userId, Guid bookingId)
    {
        var room = await _dbContext.Rooms
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == roomId);
        
        if (room == null)
            throw new Exception("Room not found");
        
        room.UserEntityId =  userId;
        room.Id = roomId;
        room.bookId = bookingId;
        
        await _dbContext.SaveChangesAsync();
    }
}