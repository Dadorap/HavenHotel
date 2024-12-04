using HavenHotel.Guests;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Display
{
    internal class DisplayBookingsDetail : IDisplayAllDetails
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
            _guestsRepo = guestRepo;
        }

        public void DisplayAll(string displayText, string isActive)
        {
            int count = 0;
            var bookings = _bookingRepo.GetAllItems();
            var rooms = _roomsRepo.GetAllItems();
            var guests = _guestsRepo.GetAllItems();

            string isAvailable = isActive.ToLower() == "true" ? "true" : "false";


            var activeBooking = from booking in bookings
                                where booking.IsActive == true
                                join guest in guests on booking.GuestId equals guest.Id
                                join room in rooms on booking.RoomId equals room.Id
                                select new
                                {
                                    CustomerName = guest.Name,
                                    room.RoomType,
                                    booking.StartDate,
                                    booking.EndDate,
                                    room.Price,
                                    IsActiveBooking = booking.IsActive,
                                };
            var deletedBookings = from booking in bookings
                                  where booking.IsActive == false
                                  join guest in guests on booking.GuestId equals guest.Id
                                  join room in rooms on booking.RoomId equals room.Id
                                  select new
                                  {
                                      CustomerName = guest.Name,
                                      room.RoomType,
                                      booking.StartDate,
                                      booking.EndDate,
                                      room.Price,
                                      IsActiveBooking = booking.IsActive,
                                  };

            var guestsList = isActive.ToLower() == "true" ? activeBooking : deletedBookings;

            Console.WriteLine("╔═════════════════╦═════════════╦════════════╦══════════════╦═══════════════╗");
            Console.WriteLine("║ Customer Name   ║ Room Type   ║ Start Date ║  End Date    ║ Total price   ║");
            Console.WriteLine("╠═════════════════╬═════════════╬════════════╬══════════════╬═══════════════╣");

            foreach (var detail in guestsList)
            {


                int totalDays = (detail.EndDate.ToDateTime(TimeOnly.MinValue) -
                    detail.StartDate.ToDateTime(TimeOnly.MinValue)).Days;
                var totalPrice = totalDays * detail.Price;

                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;

                Console.WriteLine($"║ {detail.CustomerName,-15} ║  {detail.RoomType,-10} ║ {detail.StartDate:yyyy-MM-dd} ║ {detail.EndDate:yyyy-MM-dd}   ║ {totalPrice,-13:C} ║");

                Console.ResetColor();

                if (count < guestsList.Count() - 1)
                {
                    Console.WriteLine("╠═════════════════╬═════════════╬════════════╬══════════════╬═══════════════╣");
                }

                count++;

            }


            Console.WriteLine("╚═════════════════╩═════════════╩════════════╩══════════════╩═══════════════╝");
            Console.Write("Press any key to return to the menu...");
            Console.ReadKey();
        }

    }
}
