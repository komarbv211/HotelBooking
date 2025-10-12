using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ReadModels;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories
{
    public class AdminBookingRepository: IAdminBookingRepository
    {
        private readonly string _connectionString;

        public AdminBookingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<BookingStatistics>> GetBookingStatisticsAsync()
        {
            using var db = new MySqlConnection(_connectionString);

            string sql = @"
                SELECT 
                h.Name AS HotelName,
                COUNT(b.Id) AS BookingCount,
                SUM(CASE WHEN DATE(b.CheckIn) = CURDATE() THEN 1 ELSE 0 END) AS TodayBookings,
                SUM(CASE WHEN MONTH(b.CheckIn) = MONTH(CURDATE()) AND YEAR(b.CheckIn) = YEAR(CURDATE()) THEN 1 ELSE 0 END) AS ThisMonthBookings
                FROM Bookings b
                INNER JOIN Rooms r ON b.RoomId = r.Id
                INNER JOIN Hotels h ON r.HotelId = h.Id
                GROUP BY h.Name;
            ";
            return await db.QueryAsync<BookingStatistics>(sql);
        }



        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            using var db = new MySqlConnection(_connectionString);

            string sql = @"
                SELECT b.Id, r.Number AS RoomNumber, h.Name AS HotelName, b.UserId, b.CheckIn, b.CheckOut
                FROM Bookings b
                INNER JOIN Rooms r ON b.RoomId = r.Id
                INNER JOIN Hotels h ON r.HotelId = h.Id;
            ";
            return await db.QueryAsync<Booking>(sql);
        }
    }
}
