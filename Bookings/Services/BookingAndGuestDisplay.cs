using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;

namespace HavenHotel.Bookings.Services;

public class IdDisplayHandler : IIdDisplayHandler
{
    private readonly IRepository<Room> _roomsRepo;
    private readonly IRepository<Guest> _guestRepo;
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IUserMessages _userMessages;

    public IdDisplayHandler
        (
        IRepository<Room> roomsRepo,
        IRepository<Guest> guestRepo,
        IRepository<Booking> bookingRepo,
        IUserMessages userMessages
        )
    {
        _roomsRepo = roomsRepo;
        _guestRepo = guestRepo;
        _bookingRepo = bookingRepo;
        _userMessages = userMessages;
    }

    public void DisplayRightAligned(string text)
    {
        int XOffset = 40;
        Console.SetCursorPosition(XOffset, 0);
        Console.ForegroundColor = ConsoleColor.Green;

        var rooms = _roomsRepo.GetAllItems().ToList();
        var guests = _guestRepo.GetAllItems().Where(g => g.IsActive).ToList();
        var bookings = _bookingRepo.GetAllItems().Where(b => b.IsActive).ToList();

        Console.WriteLine($"Room ID - Guest ID - Booking ID");

        var count = 0;

        foreach (var room in rooms)
        {
            var roomBookings = bookings.Where(b => b.RoomId == room.Id).ToList();

            foreach (var booking in roomBookings)
            {
                var guest = guests.FirstOrDefault(g => g.Id == booking.GuestId);
                var guestId = guest?.Id.ToString() ?? "*";

                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                Console.SetCursorPosition(XOffset, count + 1);
                Console.WriteLine($"    {room.Id}\t     {guestId}\t\t{booking.Id}");
                count++;
            }

            if (!roomBookings.Any())
            {
                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                Console.SetCursorPosition(XOffset, count + 1);
                Console.WriteLine($"    {room.Id}\t     *\t\t*");
                count++;
            }
        }

        foreach (var guest in guests)
        {
            var guestBookings = bookings.Where(b => b.GuestId == guest.Id).ToList();

            if (!guestBookings.Any())
            {
                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                Console.SetCursorPosition(XOffset, count + 1);
                Console.WriteLine($"    *\t     {guest.Id}\t\t*");
                count++;
            }
        }

        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"===== {text.ToUpper()} =====");
        Console.ResetColor();
        _userMessages.ShowCancelMessage();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
    }


}
