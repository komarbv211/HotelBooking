namespace Application.DTOs;

public class BookingStatisticsDto
{
    public int BookingCount { get; set; }
    public int TotalBookings { get; set; }
    public int TodayBookings { get; set; }
    public int ThisMonthBookings { get; set; }

}
