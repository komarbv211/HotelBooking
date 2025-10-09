namespace Application.DTOs;

public class BookingDto
{
    public string RoomNumber { get; set; } = null!;
    public string HotelName { get; set; } = null!;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
