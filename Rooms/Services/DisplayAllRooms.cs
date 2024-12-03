using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.RoomServices
{
    public class DisplayAllRooms : IDisplayAll
    {
        private IRepository<Room> _roomsRepository;

        public DisplayAllRooms(IRepository<Room> repository)
        {
            _roomsRepository = repository;
        }

        public void DisplayAll()
        {
            int count = 0;
            var rooms = _roomsRepository.GetAllItems().OrderByDescending(r => r.IsActive).ToList();

            Console.WriteLine("╔════════════╦═════════════╦════════════╦══════════════╦════════════╗");
            Console.WriteLine("║ Room Type  ║ Room Size   ║ Extra Beds ║ Total Guests ║ IsAvailable║");
            Console.WriteLine("╠════════════╬═════════════╬════════════╬══════════════╣════════════╣");

            foreach (var room in rooms)
            {
                Console.ForegroundColor = (count % 2 == 0) ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;

                Console.WriteLine($"║ {room.RoomType,-10} ║ {room.Size,-9}m² ║ {room.ExtraBed,-10} ║ {room.TotalGuests,-12} ║ {room.IsActive,-10} ║");

                Console.ResetColor();
                if (count < rooms.Count - 1)
                {
                    Console.WriteLine("╠════════════╬═════════════╬════════════╬══════════════╬════════════╣");
                }
                count++;
            }

            Console.WriteLine("╚════════════╩═════════════╩════════════╩══════════════╩════════════╝");
            Console.Write("Press any key to return to menu...");
            Console.ReadKey();
        }
    }

}