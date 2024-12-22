using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.GuestController.Update;


public class EmailUpdate : IEmailUpdate
{
    private readonly IEmailValidator _emailValidator;
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptForGuestId;
    private readonly IRepository<Guest> _guestRepo;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly Lazy<INavigationHelper> _navigationHelper;


    public EmailUpdate
        (
        IEmailValidator emailValidator,
        IErrorHandler errorHandler,
        IPromptForId promptForGuestId,
        IRepository<Guest> guestRepo,
        IUpdateConfirmation updateConfirmation,
        Lazy<INavigationHelper> navigationHelper
        )
    {
        _emailValidator = emailValidator;
        _errorHandler = errorHandler;
        _promptForGuestId = promptForGuestId;
        _guestRepo = guestRepo;
        _updateConfirmation = updateConfirmation;
        _navigationHelper = navigationHelper;
    }

    public void EmailUpdater()
    {
        while (true)
        {
            try
            {
                var id = _promptForGuestId.GetValidId("email", "guest");
                var currentGuest = _guestRepo.GetItemById(id);
                if (currentGuest == null)
                {
                    _errorHandler.DisplayError("Guest not found. Try again...");
                    continue;
                }
                Console.WriteLine($"Guest name: {currentGuest.Name}");
                Console.WriteLine($"Guest Email: {currentGuest.Email}");
                Console.Write("Enter new Email: ");
                string emailInput = Console.ReadLine().Trim();
                _navigationHelper.Value.ReturnToMenu(emailInput);
                if (!_emailValidator.IsValidEmail(emailInput) || string.IsNullOrEmpty(emailInput))
                {
                    _errorHandler.DisplayError("Invalid email input. Try again...");
                    continue;
                }

                currentGuest.Email = emailInput;
                _guestRepo.Update(currentGuest);
                _guestRepo.SaveChanges();
                _updateConfirmation.Confirmation($"The new email address {emailInput} " +
                                    $"\nhas been successfully set.");
                break;


            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError(ex.Message);
            }
        }
    }
}
