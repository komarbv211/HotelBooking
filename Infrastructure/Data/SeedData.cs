using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(
        HotelBookingDbContext context,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await context.Database.EnsureCreatedAsync();

        // === Ролі ===
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("Client"))
            await roleManager.CreateAsync(new IdentityRole("Client"));

        // === Адмін ===
        if (await userManager.FindByEmailAsync("admin@hotel.com") == null)
        {
            var admin = new AppUser
            {
                UserName = "admin@hotel.com",
                Email = "admin@hotel.com",
                BirthDate = new DateTime(1990, 1, 1),
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }

        // === Клієнт ===
        if (await userManager.FindByEmailAsync("client@hotel.com") == null)
        {
            var client = new AppUser
            {
                UserName = "client@hotel.com",
                Email = "client@hotel.com",
                BirthDate = new DateTime(1995, 5, 10),
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(client, "Client123!");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(client, "Client");
        }

        // === Готелі ===
        if (!context.Hotels.Any())
        {
            var hotel1 = new Hotel { Name = "Grand Hotel", Address = "Kyiv, Main St 1" };
            var hotel2 = new Hotel { Name = "Sea View", Address = "Odesa, Beach Rd 5" };
            await context.Hotels.AddRangeAsync(hotel1, hotel2);

            var rooms = new[]
            {
                new Room { Hotel = hotel1, Number = "101", PricePerNight = 120, Capacity = 2 },
                new Room { Hotel = hotel1, Number = "102", PricePerNight = 150, Capacity = 3 },
                new Room { Hotel = hotel2, Number = "201", PricePerNight = 200, Capacity = 2 },
                new Room { Hotel = hotel2, Number = "202", PricePerNight = 220, Capacity = 4 }
            };
            await context.Rooms.AddRangeAsync(rooms);
            await context.SaveChangesAsync();

            // === Тестові бронювання ===
            var clientUser = await userManager.FindByEmailAsync("client@hotel.com");
            if (clientUser != null)
            {
                var bookings = new[]
                {
                    new Booking
                    {
                        UserId = clientUser.Id,
                        RoomId = rooms[0].Id,
                        CheckIn = DateTime.Today.AddDays(1),
                        CheckOut = DateTime.Today.AddDays(3)
                    },
                    new Booking
                    {
                        UserId = clientUser.Id,
                        RoomId = rooms[2].Id,
                        CheckIn = DateTime.Today.AddDays(5),
                        CheckOut = DateTime.Today.AddDays(7)
                    }
                };
                await context.Bookings.AddRangeAsync(bookings);
                await context.SaveChangesAsync();
            }
        }
    }
}
