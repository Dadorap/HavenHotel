using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.Guests.GuestServices.Display;

public class DisplayGuestsDetails : IDisplayAllDetails
{
    private IRepository<Guest> _guestRepo;

    public DisplayGuestsDetails(IRepository<Guest> guestRepo)
    {
        _guestRepo = guestRepo;
    }

    public void DisplayAll(string displayText, string isActive, string id = null)
    {
        Console.Clear();
        int count = 0;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"===== {displayText.ToUpper()} =====");
        Console.ResetColor();
        var guests = _guestRepo.GetAllItems().ToList();

        Console.WriteLine("╔══════════════════╦═══════════════╦═══════════════════════════════╦═══════════╗");
        Console.WriteLine("║ Customer Name    ║ Phone Number  ║ Email                         ║ IsActive  ║");
        Console.WriteLine("╠══════════════════╬═══════════════╬═══════════════════════════════╣═══════════╣");

        foreach (var guest in guests)
        {
            if (isActive.ToLower() == "true" && guest.IsActive ||
               isActive.ToLower() == "false" && !guest.IsActive ||
               isActive.ToLower() == "all")
            {
                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
                Console.WriteLine($"║ {guest.Name,-16} ║ {guest.PhoneNumber,-13} ║ {guest.Email,-29} ║  {guest.IsActive,-8} ║");
                Console.ResetColor();
                if (count < guests.Count - 1)
                {
                    Console.WriteLine("╠══════════════════╬═══════════════╬═══════════════════════════════╬═══════════╣");
                }
                count++;
            }

        }

        Console.WriteLine("╚══════════════════╩═══════════════╩═══════════════════════════════╩═══════════╝");
        Console.Write("Press any key to return to menu...");
        Console.ReadKey();
    }


}
