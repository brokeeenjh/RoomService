namespace RoomService.Domain.Entities;

public class BookingEntity
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Guid UserEntityId { get; set; }
    public UserEntity User { get; set; }
    
    public Guid RoomEntityId { get; set; }
    public RoomEntity Room { get; set; }
}