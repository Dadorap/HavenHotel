using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace HavenHotel.Controllers.BookingController.Display
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
            _guestsRepo = guestRepo;
        }

        public void DisplayAll(string displayText, string isActive, string id = null)
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
                    booking.Room.RoomType,
                    booking.StartDate,
                    booking.EndDate,
                    booking.TotalPrice,
                    booking.Room.Price,
                    booking.Room.RoomNumber,
                    IsActiveBooking = booking.IsActive
                })
                .ToList();



            Console.WriteLine($"===== {displayText.ToUpper()} =====");


            Console.WriteLine("╔═════════════════╦════════════════════╦════════════╦══════════════╦═══════════════╗");
            Console.WriteLine("║ Customer Name   ║ Room Type/Number   ║ Start Date ║  End Date    ║ Total Price   ║");
            Console.WriteLine("╠═════════════════╬════════════════════╬════════════╬══════════════╬═══════════════╣");

            foreach (var detail in allBooking)
            {


                if (isActive.ToLower() == "true" && detail.IsActiveBooking ||
                    isActive.ToLower() == "false" && !detail.IsActiveBooking ||
                    isActive.ToLower() == "all")
                {
                    Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                    Console.WriteLine($"║ {detail.CustomerName,-15} ║  {detail.RoomType + "/" + detail.RoomNumber,-17} ║ {detail.StartDate:yyyy-MM-dd} ║ {detail.EndDate:yyyy-MM-dd}   ║ {detail.TotalPrice,-13:C} ║");
                    Console.ResetColor();

                    if (count < allBooking.Count - 1)
                    {
                        Console.WriteLine("╠═════════════════╬════════════════════╬════════════╬══════════════╬═══════════════╣");
                    }

                    count++;
                }
            }

            Console.WriteLine("╚═════════════════╩════════════════════╩════════════╩══════════════╩═══════════════╝");
            Console.Write("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
