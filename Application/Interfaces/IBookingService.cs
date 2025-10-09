using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetUserBookingsAsync(string userId);
    Task<BookingDto> CreateBookingAsync(CreateBookingDto dto);
}
