namespace RoomService.Domain.Entities;

public class RoomEntity
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    
    public Guid UserEntityId { get; set; }
    public UserEntity User { get; set; }
}