using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.Services.Display
{
    public class DisplayAllRooms : IDisplayAllDetails
    {
        private IRepository<Room> _roomsRepository;

        public DisplayAllRooms(IRepository<Room> repository)
        {
            _roomsRepository = repository;
        }

        public void DisplayAll(string displayText, string isActive)
        {
            int count = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"===== {displayText.ToUpper()} =====");
            Console.ResetColor();
            Console.WriteLine("╔══════════════╦═════════════╦═════════════╦═══════════╦══════════════╦═════════════╗");
            Console.WriteLine("║ Room Number  ║ Price/night ║ Room Type   ║ Room Size ║ Extra Beds   ║ Total Guests║");
            Console.WriteLine("╠══════════════╬═════════════╬═════════════╬═══════════╬══════════════╣═════════════╣");

            string isAvailable = isActive.ToLower() == "true" ? "true" : "false";
            var availableRoomsList= _roomsRepository.GetAllItems().Where(r => r.IsActive == true).ToList(); 
            var unAvailableRoomsList = _roomsRepository.GetAllItems().Where(r => r.IsActive == false).ToList(); 

            var rooms = isActive.ToLower() == "true" ? availableRoomsList : unAvailableRoomsList;

            foreach (var room in rooms)
            {
              
                    Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                    Console.WriteLine($"║ {room.RoomNumber,-13}║ {room.Price,-11} ║ {room.RoomType,-11} ║ {room.Size + "m²",-9} ║ {room.ExtraBed,-12} ║ {room.TotalGuests,-11} ║");

                    Console.ResetColor();
                    if (count < rooms.Count - 1)
                    {
                        Console.WriteLine("╠══════════════╬═════════════╬═════════════╬═══════════╬══════════════╬═════════════╣");
                    }
                    count++;
                

            }
                Console.WriteLine("╚══════════════╩═════════════╩═════════════╩═══════════╩══════════════╩═════════════╝");
                Console.Write("Press any key to return to menu...");
                Console.ReadKey();
        }
    }
}
