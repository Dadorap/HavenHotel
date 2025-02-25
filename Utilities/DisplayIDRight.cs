﻿using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Utilities;

public class DisplayIDRight : IDisplayRight
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomsRepo;
    private readonly IRepository<Guest> _guestsRepo;

    public DisplayIDRight(
        IRepository<Booking> bookingRepo,
        IRepository<Room> roomsRepo,
        IRepository<Guest> guestRepo)
    {
        _bookingRepo = bookingRepo;
        _roomsRepo = roomsRepo;
        _guestsRepo = guestRepo;
    }

    public void DisplayRightAligned(string text, string isActive)
    {
        Console.Clear();
        var count = 0;
        short XOffset = 40;
        Console.SetCursorPosition(XOffset, 0);
        Console.ForegroundColor = ConsoleColor.Green;
        var textDisplay = text.ToUpper();
        List<object> list = textDisplay switch
        {
            "BOOKING" => _bookingRepo.GetAllItems().Cast<object>().ToList(),
            "ROOM" => _roomsRepo.GetAllItems().Cast<object>().ToList(),
            "GUEST" => _guestsRepo.GetAllItems().Cast<object>().ToList(),
            _ => throw new ArgumentException($"Invalid input: {text}")
        };

        Console.WriteLine($"{textDisplay} ID");

        foreach (var item in list)
        {
            dynamic dynamicItem = item;

            if (isActive.ToLower() == "true" && dynamicItem.IsActive ||
                isActive.ToLower() == "false" && !dynamicItem.IsActive ||
                isActive.ToLower() == "all")
            {
                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                Console.SetCursorPosition(XOffset, count + 1);
                Console.WriteLine("    " + dynamicItem.Id);
                count++;
            }
        }
        Console.ResetColor();
    }

}
