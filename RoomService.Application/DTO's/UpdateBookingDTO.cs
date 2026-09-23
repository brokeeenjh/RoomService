namespace RoomService.Application.DTO_s;

public class UpdateBookingDTO
{
    public Guid bookingId { get; set; }
    public Guid roomId { get; set; }
    public Guid userId { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
}