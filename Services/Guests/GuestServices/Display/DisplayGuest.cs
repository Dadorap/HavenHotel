using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.Guests.GuestServices.Display;


public class DisplayGuest : IDisplay
{
    private readonly IRepository<Guest> _guestRepo;
    private readonly IDisplayRight _displayRight;
    private readonly IErrorHandler _errorHandler;
    private readonly INavigationHelper _navigationHelper;
    private readonly IUserMessages _userMessages;


    public DisplayGuest(IRepository<Guest> guestRepo,
        [KeyFilter("DisplayGuestsRight")] IDisplayRight displayRight,
        IErrorHandler errorHandler, INavigationHelper navigationHelper, IUserMessages userMessages)
    {
        _guestRepo = guestRepo;
        _displayRight = displayRight;
        _errorHandler = errorHandler;
        _navigationHelper = navigationHelper;
        _userMessages = userMessages;
    }

    public void DisplayById()
    {
        int invalidCounter = 0;
        while (true)
        {

            try
            {
                Console.Clear();
                _displayRight.DisplayRightAligned("guest");

                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("===== DISPLAY A GUEST =====");
                _userMessages.ShowCancelMessage();
                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write("Please enter the Guest's ID: ");
                string idInput = Console.ReadLine();
                _navigationHelper.ReturnToMenu(idInput);
                if (int.TryParse(idInput, out int id))
                {
                    var guest = _guestRepo.GetItemById(id);


                    Console.Clear();
                    Console.WriteLine("╔══════════════════╦═══════════════╦═══════════════════════════════╦═══════════╗");
                    Console.WriteLine("║ Customer Name    ║ Phone Number  ║ Email                         ║ IsActive  ║");
                    Console.WriteLine("╠══════════════════╬═══════════════╬═══════════════════════════════╣═══════════╣");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.WriteLine($"║ {guest.Name,-16} ║ {guest.PhoneNumber,-13} ║ {guest.Email,-29} ║  {guest.IsActive,-8} ║");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("╚══════════════════╩═══════════════╩═══════════════════════════════╩═══════════╝");

                    Console.ResetColor();

                    Console.Write("Press any key to return to menu...");
                    Console.ReadKey();
                    return;

                }
                else if (invalidCounter >= 2)
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
