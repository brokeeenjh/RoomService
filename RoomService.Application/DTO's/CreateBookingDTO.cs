namespace RoomService.Application.DTO_s;

public class CreateBookingDTO
{
    public Guid roomId { get; set; }
    public Guid userId { get; set; }
    public string Email { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
}