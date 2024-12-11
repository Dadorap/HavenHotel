using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Utilities;

public class BookingIdDisplay : IBookingIdRenderer
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IUserMessages _userMessages;

    public BookingIdDisplay
        (
        IRepository<Booking> bookingRepo,
        IUserMessages userMessages
        )
    {
        _bookingRepo = bookingRepo;
        _userMessages = userMessages;
    }

    public void DisplayBookingNumber(string text)
    {
        Console.Clear();
        int XOffset = 45;
        Console.SetCursorPosition(XOffset, 0);
        Console.ForegroundColor = ConsoleColor.Green;

        var bookings = _bookingRepo.GetAllItems().Where(r => r.IsActive).ToList();


        Console.WriteLine($"Booking ID");


        var count = 0;

        foreach (var b in bookings)
        {
            Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
            Console.SetCursorPosition(XOffset, count + 1);
            Console.WriteLine($"--> {b.Id} <--");
            count++;
        }
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"===== {text.ToUpper()} =====");
        Console.ResetColor();
        _userMessages.ShowCancelMessage();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
    }
}
