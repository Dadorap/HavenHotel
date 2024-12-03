using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests.Services.Display
{

    public class DisplayAllGuests : IDisplayAll
    {
        private IRepository<Guest> _roomsRepository;

        public DisplayAllGuests(IRepository<Guest> repository)
        {
            _roomsRepository = repository;
        }

        public void DisplayAll()
        {
            int count = 0;
            var guestsList = _roomsRepository.GetAllItems().OrderByDescending(r => r.IsActive).ToList();

            Console.WriteLine("╔══════════════════╦═══════════════╦═══════════════════════════════╦═══════════╗");
            Console.WriteLine("║ Customer Name    ║ Phone Number  ║ Email                         ║ IsActive  ║");
            Console.WriteLine("╠══════════════════╬═══════════════╬═══════════════════════════════╣═══════════╣");

            foreach (var guest in guestsList)
            {
                Console.ForegroundColor = count % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;

                Console.WriteLine($"║ {guest.Name,-16} ║ {guest.PhoneNumber,-13} ║ {guest.Email,-29} ║  {guest.IsActive,-8} ║");

                Console.ResetColor();
                if (count < guestsList.Count - 1)
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
