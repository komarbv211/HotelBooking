namespace Application.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string HotelName { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    public int Capacity { get; set; }
}
