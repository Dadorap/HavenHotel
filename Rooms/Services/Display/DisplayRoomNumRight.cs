using HavenHotel.Bookings;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.Services.Display
{
    public class DisplayRoomNumRight : IDisplayRoomNumRight
    {
        private readonly IRepository<Room> _roomsRepo;


        public DisplayRoomNumRight(IRepository<Room> roomsRepo)
        {
            _roomsRepo = roomsRepo;
        }

        public void DisplayRightAligned()
        {
            int XOffset = 40;
            Console.SetCursorPosition(XOffset, 0);
            Console.ForegroundColor = ConsoleColor.Green;        
            var list = _roomsRepo.GetAllItems().ToList();


            Console.WriteLine($"Room Number");
            var count = 0;

            foreach (var item in list)
            {
                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                Console.SetCursorPosition(XOffset, count + 1);
                Console.WriteLine("    " + item.RoomNumber);
                count++;

            }

            Console.ResetColor();
        }
    }
}
