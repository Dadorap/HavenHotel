using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;

namespace HavenHotel.Rooms.Services.Display;

public class DisplayRoomsDetails : IDisplayAllDetails
{
    private IRepository<Room> _roomsRepository;

    public DisplayRoomsDetails(IRepository<Room> repository)
    {
        _roomsRepository = repository;
    }

    public void DisplayAll(string displayText, string isActive)
    {
        Console.Clear();
        int count = 0;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"===== {displayText.ToUpper()} =====");
        Console.ResetColor();
        Console.WriteLine("╔══════════════╦═════════════╦═════════════╦═══════════╦══════════════╦═════════════╗");
        Console.WriteLine("║ Room Number  ║ Price/night ║ Room Type   ║ Room Size ║ Extra Beds   ║ Total Guests║");
        Console.WriteLine("╠══════════════╬═════════════╬═════════════╬═══════════╬══════════════╣═════════════╣");

        var rooms = _roomsRepository.GetAllItems().ToList();
        



        foreach (var room in rooms)
        {
            if ((isActive.ToLower() == "true" && room.IsActive) ||
                (isActive.ToLower() == "false" && !room.IsActive) ||
                (isActive.ToLower() == "all"))
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


        }
        Console.WriteLine("╚══════════════╩═════════════╩═════════════╩═══════════╩══════════════╩═════════════╝");
        Console.Write("Press any key to return to menu...");
        Console.ReadKey();
    }
}
