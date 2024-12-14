using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Data;
using HavenHotel.Models;
using HavenHotel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Data.SeedingData
{
    public class Seed
    {
        public static void Seedings()
        {
            var container = DependencyContainer.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var dbContext = scope.Resolve<HotelDbContext>();
                if (dbContext.Rooms.Any() || dbContext.Guests.Any() || dbContext.Bookings.Any()) return;

                var guestsToAdd = new List<Guest>
                {
                    new Guest { Name = "John Doe", PhoneNumber = "1234567890", Email = "john.doe@example.com", IsActive = true },
                    new Guest { Name = "Jane Smith", PhoneNumber = "9876543210", Email = "jane.smith@example.com", IsActive = true },
                    new Guest { Name = "Michael Johnson", PhoneNumber = "5556667777", Email = "michael.johnson@example.com", IsActive = true },
                    new Guest { Name = "Emily Brown", PhoneNumber = "4443332222", Email = "emily.brown@example.com", IsActive = true }
                };

                dbContext.AddRange(guestsToAdd);

                var roomsToAdd = new List<Room>
                {
                    new Room
                    {
                        RoomNumber = 101,
                        Price = 1200.00m,
                        RoomType = RoomType.SINGLE,
                        ExtraBed = 0, Size = 15,
                        TotalGuests = 1,
                        IsAvailable = false
                    },
                    new Room
                    {
                        RoomNumber = 102,
                        Price = 1200.00m,
                        RoomType = RoomType.SINGLE,
                        ExtraBed = 0, Size = 15,
                        TotalGuests = 1,
                        IsAvailable = true
                    },
                    new Room
                    {
                        RoomNumber = 103,
                        Price = 2000.00m,
                        RoomType = RoomType.DOUBLE,
                        ExtraBed = 1, Size = 25,
                        TotalGuests = 2,
                        IsAvailable = false
                    },
                    new Room
                    {
                        RoomNumber = 104,
                        Price = 3500.00m,
                        RoomType = RoomType.SUITE,
                        ExtraBed = 2, Size = 40,
                        TotalGuests = 4,
                        IsAvailable = false
                    },
                    new Room
                    {
                        RoomNumber = 105,
                        Price = 4500.00m,
                        RoomType = RoomType.FAMILY,
                        ExtraBed = 2, Size = 50,
                        TotalGuests = 6,
                        IsAvailable = false
                    }
                };

                dbContext.AddRange(roomsToAdd);


                dbContext.SaveChanges();

                var roomIds = dbContext.Rooms.ToList();
                var guestIds = dbContext.Guests.ToList();
                var bookingsToAdd = new List<Booking>
                {
                    new Booking
                    {
                        StartDate = new DateOnly(2024, 11, 1),
                        EndDate = new DateOnly(2024, 12, 20),
                        TotalPrice = 171500m,
                        RoomId = 1,
                        GuestId = 1,
                        IsPaid = true,
                        IsActive = true,

                    },
                    new Booking
                    {
                        StartDate = new DateOnly(2024, 12, 1),
                        EndDate = new DateOnly(2025, 01, 30),
                        TotalPrice=120000m,
                        RoomId = 3,
                        GuestId = 2,
                        IsPaid = false,
                        IsActive = true,
                    },
                    new Booking
                    {
                        StartDate = new DateOnly(2024, 11, 1),
                        EndDate = new DateOnly(2024, 12, 13),
                        TotalPrice =154000m,
                        RoomId = 4,
                        GuestId = 3,
                        IsPaid = false,
                        IsActive = true,
                    },
                    new Booking
                    {
                        StartDate = new DateOnly(2024, 10, 1),
                        EndDate = new DateOnly(2024, 12, 20),
                        TotalPrice =360000m,
                        RoomId = 5,
                        GuestId = 4,
                        IsPaid = false,
                        IsActive = true,

                    }

                };

                dbContext.AddRange(bookingsToAdd);


                dbContext.SaveChanges();
            }
        }
    }

}
