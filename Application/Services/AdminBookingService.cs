using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services;

public class AdminBookingService : IAdminBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IAdminBookingRepository _adminBookingRepository;
    public AdminBookingService(IBookingRepository bookingRepository, IAdminBookingRepository adminBookingRepository)
    {
        _bookingRepository = bookingRepository;
        _adminBookingRepository = adminBookingRepository;
    }
    public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllBookingsAsync();

        return bookings.Select(b => new BookingDto
        {
            RoomNumber = b.Room.Number,
            HotelName = b.Room.Hotel.Name,
            CheckIn = b.CheckIn,
            CheckOut = b.CheckOut
        });
    }

    public async Task<BookingStatisticsDto> GetBookingStatisticsAsync()
    {
        var bookings = await _bookingRepository.GetAllBookingsAsync();
        var now = DateTime.Now;

        return new BookingStatisticsDto
        {
            TotalBookings = bookings.Count(),
            TodayBookings = bookings.Count(b => b.CheckIn.Date == now.Date),
            ThisMonthBookings = bookings.Count(b => b.CheckIn.Year == now.Year && b.CheckIn.Month == now.Month)
        };
    }

}
