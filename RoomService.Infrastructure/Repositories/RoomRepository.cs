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
            .Include(x => x.RoomEntity)
            .FirstOrDefaultAsync( x => x.Id == id );

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        return user.RoomEntity;
    }

    public async Task<IEnumerable<RoomEntity>> FreeRoomsAsync()
    {
       var rooms =  await _dbContext.Rooms
           .Where(x => x.IsActive == true)
           .Where(x => x.EndDate <= DateTime.UtcNow || x.StartDate >= DateTime.UtcNow)
            .ToListAsync();
       return rooms;
    }

    public async Task DeleteRoomAsync(Guid id)
    {
        var room = await _dbContext.Rooms
            .FirstOrDefaultAsync(x => x.Id == id);
        
        _dbContext.Rooms.Remove(room);
    }
}