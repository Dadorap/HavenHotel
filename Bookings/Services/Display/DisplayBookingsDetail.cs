using HavenHotel.Guests;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HavenHotel.Bookings.Services.Display
{
    public class DisplayBookingsDetail : IDisplayAllDetails
    {
        private readonly IRepository<Booking> _bookingRepo;
        private readonly IRepository<Room> _roomsRepo;
        private readonly IRepository<Guest> _guestsRepo;

        public DisplayBookingsDetail(
            IRepository<Booking> bookingRepo,
            IRepository<Room> roomsRepo,
            IRepository<Guest> guestRepo)
        {
            _bookingRepo = bookingRepo;
            _roomsRepo = roomsRepo;
            _guestsRepo = guestRepo ;
        }

        public void DisplayAll(string displayText, string isActive)
        {
            Console.Clear();
            int count = 0;

            var bookings = _bookingRepo.GetAllItems();
            var rooms = _roomsRepo.GetAllItems();
            var guests = _guestsRepo.GetAllItems();



            var allBooking = bookings
                .AsQueryable()
                .Include(booking => booking.Room)
                .Include(booking => booking.Guest)
                .Select(booking => new
                {
                    CustomerName = booking.Guest.Name,
                    RoomType = booking.Room.RoomType,
                    booking.StartDate,
                    booking.EndDate,
                    Price = booking.Room.Price,
                    IsActiveBooking = booking.IsActive
                })
                .ToList();





            Console.WriteLine("╔═════════════════╦═════════════╦════════════╦══════════════╦═══════════════╗");
            Console.WriteLine("║ Customer Name   ║ Room Type   ║ Start Date ║  End Date    ║ Total Price   ║");
            Console.WriteLine("╠═════════════════╬═════════════╬════════════╬══════════════╬═══════════════╣");

            foreach (var detail in allBooking)
            {
                int totalDays = (detail.EndDate.ToDateTime(TimeOnly.MinValue) -
                   detail.StartDate.ToDateTime(TimeOnly.MinValue)).Days;
                var totalPrice = totalDays * detail.Price;

                if ((isActive.ToLower() == "true" && detail.IsActiveBooking) ||
                    (isActive.ToLower() == "false" && !detail.IsActiveBooking) ||
                    (isActive.ToLower() == "all"))
                {
                    Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                    Console.WriteLine($"║ {detail.CustomerName,-15} ║  {detail.RoomType,-10} ║ {detail.StartDate:yyyy-MM-dd} ║ {detail.EndDate:yyyy-MM-dd}   ║ {totalPrice,-13:C} ║");
                    Console.ResetColor();

                    if (count < allBooking.Count - 1)
                    {
                        Console.WriteLine("╠═════════════════╬═════════════╬════════════╬══════════════╬═══════════════╣");
                    }

                    count++;
                }
            }

            Console.WriteLine("╚═════════════════╩═════════════╩════════════╩══════════════╩═══════════════╝");
            Console.Write("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
