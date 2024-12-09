using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.Guests.GuestServices.Update;


public class EmailUpdater : IEmailUpdater
{
    private readonly IEmailValidator _emailValidator;
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForGuestId _promptForGuestId;
    private readonly IRepository<Guest> _guestRepo;
    private readonly Lazy<IMenu> _mainMenu;

    public EmailUpdater
        (
        IEmailValidator emailValidator,
        IErrorHandler errorHandler,
        IPromptForGuestId promptForGuestId,
        IRepository<Guest> guestRepo,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu

        )
    {
        _emailValidator = emailValidator;
        _errorHandler = errorHandler;
        _promptForGuestId = promptForGuestId;
        _guestRepo = guestRepo;
        _mainMenu = mainMenu;
    }

    public void EmailUpdate()
    {
        while (true)
        {
            try
            {
                var id = _promptForGuestId.GetValidGuestId("email");
                var currentGuest = _guestRepo.GetItemById(id);
                Console.WriteLine($"Guest name: {currentGuest.Name}");
                Console.WriteLine($"Guest Email: {currentGuest.Email}");
                Console.Write("Enter new Email: ");
                string emailInput = Console.ReadLine();
                if (!_emailValidator.IsValidEmail(emailInput))
                {
                    _errorHandler.DisplayError("Invalid email input. Try again...");
                    continue;
                }

                currentGuest.Email = emailInput;
                _guestRepo.SaveChanges();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The new email address {emailInput} " +
                    $"\nhas been successfully set.");
                Console.ResetColor();
                Console.Write("Press any key to return to menu...");
                Console.ReadKey();
                _mainMenu.Value.DisplayMenu();
                break;


            }
            catch (Exception)
            {
                _errorHandler.DisplayError("");
            }
        }
    }
}
