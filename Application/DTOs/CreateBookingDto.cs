namespace Application.DTOs;

public class CreateBookingDto
{
    public string UserId { get; set; } = null!;
    public int RoomId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
