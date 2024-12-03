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
    public class Seed
    {
        public void Seedings()
        {
            var container = DependencyContainer.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var dbContext = scope.Resolve<HotelDbContext>();

                var guestsToAdd = new List<Guests.Guest>
                {
                    new Guests.Guest { Name = "John Doe", PhoneNumber = "1234567890", Email = "john.doe@example.com", IsActive = true },
                    new Guests.Guest { Name = "Jane Smith", PhoneNumber = "9876543210", Email = "jane.smith@example.com", IsActive = true },
                    new Guests.Guest { Name = "Michael Johnson", PhoneNumber = "5556667777", Email = "michael.johnson@example.com", IsActive = true },
                    new Guests.Guest { Name = "Emily Brown", PhoneNumber = "4443332222", Email = "emily.brown@example.com", IsActive = true }
                };

                foreach (var guest in guestsToAdd)
                {
                    if (dbContext.Guests.Any(g => g.Name == guest.Name && g.PhoneNumber == guest.PhoneNumber)) continue;
                    dbContext.Guests.Add(guest);
                }

                var roomsToAdd = new List<Room>
                {
                    new Room {RoomNumber = 101, Price = 1200.00m, RoomType = RoomType.SINGLE, ExtraBed = 0, Size = 15, TotalGuests = 1, IsActive = true },
                    new Room {RoomNumber = 102, Price = 1200.00m, RoomType = RoomType.SINGLE, ExtraBed = 0, Size = 15, TotalGuests = 1, IsActive = false },
                    new Room {RoomNumber = 103, Price = 2000.00m, RoomType = RoomType.DOUBLE, ExtraBed = 1, Size = 25, TotalGuests = 2, IsActive = false },
                    new Room {RoomNumber = 104, Price = 3500.00m, RoomType = RoomType.SUITE, ExtraBed = 2, Size = 40, TotalGuests = 4, IsActive = false },
                    new Room {RoomNumber = 105, Price = 4500.00m, RoomType = RoomType.FAMILY, ExtraBed = 2, Size = 50, TotalGuests = 6, IsActive = false }
                };

                foreach (var room in roomsToAdd)
                {
                    if (dbContext.Rooms.Any(r => r.Price == room.Price && r.RoomType == room.RoomType && r.Size == room.Size)) continue;
                    dbContext.Rooms.Add(room);
                }

                dbContext.SaveChanges();

                var roomIds = dbContext.Rooms.ToList();
                var guestIds = dbContext.Guests.ToList();
                var bookingsToAdd = new List<Booking>
                {
                    new Booking
                    {
                        StartDate = new DateOnly(2024, 11, 1),
                        EndDate = new DateOnly(2024, 12, 20),
                        RoomId = roomIds.FirstOrDefault(r => r.RoomType == RoomType.SUITE)?.Id ?? 0,
                        GuestId = guestIds.FirstOrDefault(g => g.Name == "John Doe")?.Id ?? 0,
                        IsActive = true,
                    
                    },
                    new Booking
                    {
                        StartDate = new DateOnly(2024, 12, 1),
                        EndDate = new DateOnly(2025, 01, 30),
                        RoomId = roomIds.FirstOrDefault(r => r.RoomType == RoomType.DOUBLE)?.Id ?? 0,
                        GuestId = guestIds.FirstOrDefault(g => g.Name == "Jane Smith")?.Id ?? 0,
                        IsActive = true,
                    },
                    new Booking
                    {
                        StartDate = new DateOnly(2024, 11, 1),
                        EndDate = new DateOnly(2024, 12, 15),
                        RoomId = roomIds.FirstOrDefault(r => r.RoomType == RoomType.SUITE)?.Id ?? 0,
                        GuestId = guestIds.FirstOrDefault(g => g.Name == "Michael Johnson")?.Id ?? 0,
                        IsActive = true,
                    },
                    new Booking
                    {
                        StartDate = new DateOnly(2024, 10, 1),
                        EndDate = new DateOnly(2024, 12, 20),
                        RoomId = roomIds.FirstOrDefault(r => r.RoomType == RoomType.FAMILY)?.Id ?? 0,
                        GuestId = guestIds.FirstOrDefault(g => g.Name == "Emily Brown")?.Id ?? 0,
                        IsActive = true,

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
