using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Linq;

namespace HavenHotel.Bookings.BookingServices
{
    public class DisplayAllBookings : IDisplayAll
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Room> _roomsRepository;
        private readonly IRepository<Guest> _guestsRepository;

        public DisplayAllBookings(IRepository<Booking> bookingRepo, IRepository<Room> roomsRepo, IRepository<Guest> guestRepo)
        {
            _bookingRepository = bookingRepo;
            _roomsRepository = roomsRepo;
            _guestsRepository = guestRepo;
        }

        public void DisplayAll()
        {
            int count = 0;
            var bookings = _bookingRepository.GetAllItems();
            var rooms = _roomsRepository.GetAllItems();
            var guests = _guestsRepository.GetAllItems();


            var bookingDetails = from booking in bookings
                                 join guest in guests on booking.GuestId equals guest.Id
                                 join room in rooms on booking.RoomId equals room.Id
                                 select new
                                 {
                                     CustomerName = guest.Name,
                                     RoomType = room.RoomType,
                                     StartDate = booking.StartDate,
                                     EndDate = booking.EndDate,
                                     Price = room.Price,
                                 };

            // Display table headers
            Console.WriteLine("╔═════════════════╦═════════════╦════════════╦══════════════╦══════════════╗");
            Console.WriteLine("║ Customer Name   ║ Room Type   ║ Start Date ║  End Date    ║ Total price  ║");
            Console.WriteLine("╠═════════════════╬═════════════╬════════════╬══════════════╬══════════════╣");

            // Display each booking in the table
            foreach (var detail in bookingDetails)
            {
            int totalDays = (detail.EndDate.ToDateTime(TimeOnly.MinValue) - detail.StartDate.ToDateTime(TimeOnly.MinValue)).Days;
            var totalPrice = totalDays * detail.Price;
                // Alternate row colors
                Console.ForegroundColor = (count % 2 == 0) ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;

                // Display booking details
                Console.WriteLine($"║ {detail.CustomerName,-15} ║  {detail.RoomType,-10} ║ {detail.StartDate:yyyy-MM-dd} ║ {detail.EndDate:yyyy-MM-dd}   ║ {totalPrice,-10:C} ║");

                Console.ResetColor();

                // Add separator for all rows except the last one
                if (count < bookingDetails.Count() - 1)
                {
                    Console.WriteLine("╠═════════════════╬═════════════╬════════════╬══════════════╬══════════════╣");
                }

                count++;
            }

            // Footer
            Console.WriteLine("╚═════════════════╩═════════════╩════════════╩══════════════╩══════════════╝");
            Console.Write("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
