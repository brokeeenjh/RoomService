namespace RoomService.Domain.Entities;

public class RoomEntity
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    
    public BookingEntity  BookingEntity { get; set; }
    public Guid bookId { get; set; }
    public Guid UserEntityId { get; set; }
    public UserEntity User { get; set; }
}