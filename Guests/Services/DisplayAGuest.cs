using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests.GuestServices
{

    public class DisplayAGuest : IDisplay
    {
        private readonly IRepository<Guest> _guestRepo;
        private readonly IDisplayRight _displayRight;
        private readonly IErrorHandler _errorHandler;
        public DisplayAGuest(IRepository<Guest> guestRepo, [KeyFilter("DisplayGuestsRight")] IDisplayRight displayRight, IErrorHandler errorHandler)
        {
            _guestRepo = guestRepo;
            _displayRight = displayRight;
            _errorHandler = errorHandler;
        }

        public void DisplayById()
        {
                    int invalidCounter = 0;
            while (true)
            {
                try
                {

                    Console.Clear();
                    _displayRight.DisplayRightAligned();
                    var guests = _guestRepo.GetAllItems().ToList().Count;

                    Console.SetCursorPosition(0, 0);

                    Console.Write("Please enter the Guest's ID: ");
                    if (int.TryParse(Console.ReadLine(), out int id) && id !> guests && id !< guests)
                    {
                        var guest = _guestRepo.GetItemById(id);
                        Console.Write("Guests name: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{guest.Name}");
                        Console.ResetColor();
                        Console.Write("Phone Number: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{guest.PhoneNumber}");
                        Console.ResetColor();
                        Console.Write("Email: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{guest.Email}");
                        Console.ResetColor();                     
                           
                        Console.Write("Press any key to return to menu...");
                        Console.ReadKey();
                        break;
                    }
                    else if (invalidCounter >= 1)
                    {
                        _errorHandler.DisplayError("List's on the right ->. " +
                            "\nChoose an ID from the list please:)");
                        invalidCounter = 0;
                    }
                    else
                    {
                        invalidCounter++;
                        _errorHandler.DisplayError("Invalid id input, try again...");
                    }


                }
                catch (Exception)
                {
                    invalidCounter++;
                    _errorHandler.DisplayError("Invalid id number input, try again...");
                }
            }
        }
    }
}
