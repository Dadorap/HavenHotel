using HavenHotel.Bookings;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HavenHotel.Common
{
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

        public void DisplayRightAligned(string text)
        {
            int XOffset = 40;
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
            var count = 0;

            foreach (var item in list)
            {
                dynamic dynamicItem = item;

                if (dynamicItem.IsActive)
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
}
