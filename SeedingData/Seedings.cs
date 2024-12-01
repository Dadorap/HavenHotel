using Autofac;
using HavenHotel.Bookings;
using HavenHotel.Configuration;
using HavenHotel.Data;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.SeedingData
{
    public class Seedings
    {
        public void Seed()
        {
            var container = DependencyContainer.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var dbContext = scope.Resolve<HotelDbContext>();

                // Seed Guests
                var guestsToAdd = new List<Guests.Guest>
            {
                new Guests.Guest { Name = "JOHN DOE", PhoneNumber = "1234567890", Email = "john.doe@example.com", IsActive = true },
                new Guests.Guest { Name = "JANE SMITH", PhoneNumber = "9876543210", Email = "jane.smith@example.com", IsActive = true },
                new Guests.Guest { Name = "MIKE JOHANNSON", PhoneNumber = "5556667777", Email = "michael.johnson@example.com", IsActive = true },
                new Guests.Guest { Name = "EMILY BROWN", PhoneNumber = "4443332222", Email = "emily.brown@example.com", IsActive = true }
            };

                foreach (var guest in guestsToAdd)
                {
                    if (dbContext.Guests.Any(g => g.Name == guest.Name && g.PhoneNumber == guest.PhoneNumber)) continue;
                    dbContext.Guests.Add(guest);
                }

                // Seed Rooms
                var roomsToAdd = new List<Room>
            {
                new Room { Price = 120.50m, RoomType = RoomType.Single, ExtraBed = 0, Size = 15, TotalGuests = 1, IsAvailable = false },
                new Room { Price = 200.00m, RoomType = RoomType.Double, ExtraBed = 1, Size = 25, TotalGuests = 2, IsAvailable = false },
                new Room { Price = 350.00m, RoomType = RoomType.Suite, ExtraBed = 2, Size = 40, TotalGuests = 4, IsAvailable = false },
                new Room { Price = 450.00m, RoomType = RoomType.Family, ExtraBed = 2, Size = 50, TotalGuests = 6, IsAvailable = false }
            };

                foreach (var room in roomsToAdd)
                {
                    if (dbContext.Rooms.Any(r => r.Price == room.Price && r.RoomType == room.RoomType && r.Size == room.Size)) continue;
                    dbContext.Rooms.Add(room);
                }

                dbContext.SaveChanges();

                // Add Bookings
                var roomIds = dbContext.Rooms.ToList();
                var guestIds = dbContext.Guests.ToList();
                var bookingsToAdd = new List<Booking>
            {
                new Booking
                {
                    StartDate = new DateOnly(2024, 11, 1),
                    EndDate = new DateOnly(2024, 12, 20),
                    RoomId = roomIds.FirstOrDefault(r => r.RoomType == RoomType.Single)?.Id ?? 0,
                    GuestId = guestIds.FirstOrDefault(g => g.Name == "John Doe")?.Id ?? 0
                },
                new Booking
                {
                    StartDate = new DateOnly(2024, 12, 1),
                    EndDate = new DateOnly(2025, 01, 30),
                    RoomId = roomIds.FirstOrDefault(r => r.RoomType == RoomType.Double)?.Id ?? 0,
                    GuestId = guestIds.FirstOrDefault(g => g.Name == "Jane Smith")?.Id ?? 0
                },
                new Booking
                {
                    StartDate = new DateOnly(2024, 11, 1),
                    EndDate = new DateOnly(2024, 12, 15),
                    RoomId = roomIds.FirstOrDefault(r => r.RoomType == RoomType.Suite)?.Id ?? 0,
                    GuestId = guestIds.FirstOrDefault(g => g.Name == "Michael Johnson")?.Id ?? 0
                },
                new Booking
                {
                    StartDate = new DateOnly(2024, 10, 1),
                    EndDate = new DateOnly(2024, 12, 20),
                    RoomId = roomIds.FirstOrDefault(r => r.RoomType == RoomType.Family)?.Id ?? 0,
                    GuestId = guestIds.FirstOrDefault(g => g.Name == "Emily Brown")?.Id ?? 0
                }

            };

                foreach (var booking in bookingsToAdd)
                {
                    if (booking.RoomId == 0 || booking.GuestId == 0) continue;
                    if (dbContext.Bookings.Any(b => b.RoomId == booking.RoomId && b.StartDate <= booking.EndDate && b.EndDate >= booking.StartDate)) continue;

                    dbContext.Bookings.Add(booking);
                }

                dbContext.SaveChanges();
            }
        }
    }

}
