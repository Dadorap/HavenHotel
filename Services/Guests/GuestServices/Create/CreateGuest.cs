using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.Guests.GuestServices.Create;

public class CreateGuest : ICreate
{
    private readonly IRepository<Guest> _guestRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly IEmailValidator _emailValidator;


    public CreateGuest(
        IRepository<Guest> guestRepo,
        Lazy<INavigationHelper> navigationHelper,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        IErrorHandler errorHandler,
        IUserMessages userMessages,
        IEmailValidator emailValidator
        )
    {
        _guestRepo = guestRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _userMessages = userMessages;
        _mainMenu = mainMenu;
        _emailValidator = emailValidator;
    }

    public void Create()
    {

        while (true)
        {
            Console.Clear();
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("CREATE NEW GUEST");
                Console.ResetColor();
                _userMessages.ShowCancelMessage();
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.Write("Enter guest's full name: ");
                var guestName = Console.ReadLine()?.Trim();
                _navigationHelper.Value.ReturnToMenu(guestName);
                if (string.IsNullOrWhiteSpace(guestName))
                {
                    _errorHandler.DisplayError("Invalid name. Please enter a valid name.");
                    continue;
                }

                Console.Write("Enter phone number: ");
                string phoneNumber = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(phoneNumber);
                if (string.IsNullOrWhiteSpace(phoneNumber)
                    || phoneNumber.Length < 3
                    || phoneNumber.Length > 15
                    || !phoneNumber.All(char.IsDigit))
                {
                    _errorHandler.DisplayError("Invalid phone number. " +
                        "\nPlease enter a number between 3 and 15 digits.");
                    continue;
                }

                Console.Write("Enter email address: ");
                var email = Console.ReadLine()?.Trim();
                _navigationHelper.Value.ReturnToMenu(email);

                if (string.IsNullOrWhiteSpace(email) || !_emailValidator.IsValidEmail(email))
                {
                    _errorHandler.DisplayError("Invalid email address. " +
                        "\nPlease enter a valid email.");
                    continue;
                }

                var guest = new Guest
                {
                    Name = guestName,
                    PhoneNumber = phoneNumber,
                    Email = email
                };

                _guestRepo.Add(guest);
                _guestRepo.SaveChanges();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nGuest successfully created:");
                Console.WriteLine($"Name: {guest.Name}");
                Console.WriteLine($"Phone: {guest.PhoneNumber}");
                Console.WriteLine($"Email: {guest.Email}");
                Console.ResetColor();
                Console.Write("Press any key to return to menu...");
                Console.ReadKey();
                _mainMenu.Value.DisplayMenu();
                break;
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError($"An error occurred: {ex.Message}");
            }
        }
    }


}
