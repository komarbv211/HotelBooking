using Domain.Entities;

namespace Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(HotelBookingDbContext context)
    {
        // Створюємо базу, якщо ще не існує
        context.Database.EnsureCreated();

        if (!context.Hotels.Any())
        {
            // Готелі
            var hotel1 = new Hotel { Name = "Grand Hotel", Address = "Kyiv, Main St 1" };
            var hotel2 = new Hotel { Name = "Sea View", Address = "Odesa, Beach Rd 5" };
            context.Hotels.AddRange(hotel1, hotel2);

            // Номери
            var room1 = new Room { Hotel = hotel1, Number = "101", PricePerNight = 120, Capacity = 2 };
            var room2 = new Room { Hotel = hotel1, Number = "102", PricePerNight = 150, Capacity = 3 };
            var room3 = new Room { Hotel = hotel2, Number = "201", PricePerNight = 200, Capacity = 2 };
            var room4 = new Room { Hotel = hotel2, Number = "202", PricePerNight = 220, Capacity = 4 };
            context.Rooms.AddRange(room1, room2, room3, room4);

            context.SaveChanges();

            // Тестові бронювання
            var booking1 = new Booking
            {
                UserId = "test-user",
                RoomId = room1.Id,
                CheckIn = DateTime.Today.AddDays(1),
                CheckOut = DateTime.Today.AddDays(3)
            };
            var booking2 = new Booking
            {
                UserId = "test-user",
                RoomId = room3.Id,
                CheckIn = DateTime.Today.AddDays(5),
                CheckOut = DateTime.Today.AddDays(7)
            };
            context.Bookings.AddRange(booking1, booking2);

            context.SaveChanges();
        }
    }
}
