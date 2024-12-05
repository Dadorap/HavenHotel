using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests.Services.Display
{
    public class DisplayGuestsDetails : IDisplayAllDetails
    {
        private IRepository<Guest> _guestRepo;

        public DisplayGuestsDetails(IRepository<Guest> guestRepo)
        {
            _guestRepo = guestRepo;
        }

        public void DisplayAll(string displayText, string isActive)
        {
            Console.Clear();
            int count = 0;
          
            string isAvailable = isActive.ToLower() == "true" ? "true" : "false";
            var availableRoomsList = _guestRepo.GetAllItems().Where(r => r.IsActive == true).ToList();
            var unAvailableRoomsList = _guestRepo.GetAllItems().Where(r => r.IsActive == false).ToList();
            var guests = isActive.ToLower() == "true" ? availableRoomsList : unAvailableRoomsList;

            Console.WriteLine("╔══════════════════╦═══════════════╦═══════════════════════════════╦═══════════╗");
            Console.WriteLine("║ Customer Name    ║ Phone Number  ║ Email                         ║ IsActive  ║");
            Console.WriteLine("╠══════════════════╬═══════════════╬═══════════════════════════════╣═══════════╣");

            foreach (var guest in guests)
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

            Console.WriteLine("╚══════════════════╩═══════════════╩═══════════════════════════════╩═══════════╝");
            Console.Write("Press any key to return to menu...");
            Console.ReadKey();
        }


    }
}
