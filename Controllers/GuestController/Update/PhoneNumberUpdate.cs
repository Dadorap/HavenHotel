using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.GuestController.Update;

public class PhoneNumberUpdate : IPhoneNumberUpdate
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptForGuestId;
    private readonly IRepository<Guest> _guestRepo;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly Lazy<INavigationHelper> _navigationHelper;


    public PhoneNumberUpdate
        (
        IErrorHandler errorHandler,
        IPromptForId promptForGuestId,
        IRepository<Guest> guestRepo,
        IUpdateConfirmation updateConfirmation,
        Lazy<INavigationHelper> navigationHelper)
    {
        _errorHandler = errorHandler;
        _promptForGuestId = promptForGuestId;
        _guestRepo = guestRepo;
        _updateConfirmation = updateConfirmation;
        _navigationHelper = navigationHelper;
    }

    public void PhoneNumberUpdater()
    {
        while (true)
        {
            try
            {
                var id = _promptForGuestId.GetValidId("phone number", "guest");
                var currentGuest = _guestRepo.GetItemById(id);
                if (currentGuest == null)
                {
                    _errorHandler.DisplayError("Guest not found. Try again...");
                    continue;
                }
                Console.WriteLine($"Guest name: {currentGuest.Name}");
                Console.WriteLine($"Guest phone number: {currentGuest.PhoneNumber}");
                Console.Write("Enter new phone number: ");
                string numInput = Console.ReadLine().Trim();
                _navigationHelper.Value.ReturnToMenu(numInput);
                if (string.IsNullOrEmpty(numInput.Trim())
                    || !numInput.All(char.IsDigit)
                    || numInput.Length < 3 || numInput.Length > 15
                    )
                {
                    _errorHandler.DisplayError("Invalid phone number input. Try again...");
                    continue;
                }

                currentGuest.PhoneNumber = numInput;
                _guestRepo.Update(currentGuest);
                _updateConfirmation.Confirmation($"The new email address {numInput} " +
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
