using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests
{
    public class DisplayGuestsRight : IDisplayRight
    {
        private readonly IRepository<Guest> _guestRepo;

        public DisplayGuestsRight(IRepository<Guest> guestRepo) 
        { 
            _guestRepo = guestRepo;
        }
        public void DisplayRightAligned()
        {
            int XOffset = 40;
            var guestList = _guestRepo.GetAllItems().ToList();
            Console.SetCursorPosition(XOffset, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Guests ID");

            for (int i = 0; i < guestList.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                Console.SetCursorPosition(XOffset, i + 1);
                Console.WriteLine("    "+guestList[i].Id);
            }

            Console.ResetColor();

        }
    }
}
