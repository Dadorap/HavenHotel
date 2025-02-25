﻿using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Utilities;

public class BookingSidebarDisplay : IBookingSidebarDisplay
{
    private readonly IRepository<Room> _roomsRepo;
    private readonly IRepository<Guest> _guestRepo;
    private readonly IUserMessages _userMessages;

    public BookingSidebarDisplay
        (
        IRepository<Room> roomRepo,
        IRepository<Guest> guestRepo,
        IUserMessages userMessages
        )
    {
        _roomsRepo = roomRepo;
        _guestRepo = guestRepo;
        _userMessages = userMessages;
    }

    public void DisplayRightAligned(string text)
    {
        int XOffset = 40;
        Console.SetCursorPosition(XOffset, 0);
        Console.ForegroundColor = ConsoleColor.Green;

        var rooms = _roomsRepo.GetAllItems().ToList();
        var guests = _guestRepo.GetAllItems().Where(g => g.IsActive).ToList();
        var header = $"available rooms - Guest ID".ToUpper();
        Console.WriteLine(header);

        var roomGuestPairs = rooms.Zip(guests, (room, guest) => new { room.RoomNumber, room.TotalGuests, guest.Id });

        var count = 0;

        foreach (var pair in roomGuestPairs)
        {
            Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
            Console.SetCursorPosition(XOffset, count + 1);
            Console.WriteLine($"    {pair.RoomNumber} / {pair.TotalGuests}  \t\t{pair.Id}");
            count++;
        }

        if (rooms.Count > guests.Count)
        {
            foreach (var room in rooms.Skip(guests.Count))
            {
                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                Console.SetCursorPosition(XOffset, count + 1);
                Console.WriteLine($"    {room.RoomNumber} / {room.TotalGuests}  \t\t*");
                count++;
            }
        }
        else if (guests.Count > rooms.Count)
        {
            foreach (var guest in guests.Skip(rooms.Count))
            {
                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                Console.SetCursorPosition(XOffset, count + 1);
                Console.WriteLine($"    * / *  \t\t{guest.Id}");
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
