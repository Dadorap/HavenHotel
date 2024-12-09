using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.Guests.GuestServices.Update;


public class EmailUpdate : IEmailUpdate
{
    private readonly IEmailValidator _emailValidator;
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForGuestId _promptForGuestId;
    private readonly IRepository<Guest> _guestRepo;
    private readonly IUpdateConfirmation _updateConfirmation;

    public EmailUpdate
        (
        IEmailValidator emailValidator,
        IErrorHandler errorHandler,
        IPromptForGuestId promptForGuestId,
        IRepository<Guest> guestRepo,
        IUpdateConfirmation updateConfirmation

        )
    {
        _emailValidator = emailValidator;
        _errorHandler = errorHandler;
        _promptForGuestId = promptForGuestId;
        _guestRepo = guestRepo;
        _updateConfirmation = updateConfirmation;
    }

    public void EmailUpdater()
    {
        while (true)
        {
            try
            {
                var id = _promptForGuestId.GetValidGuestId("email");
                var currentGuest = _guestRepo.GetItemById(id);
                if (currentGuest == null)
                {
                    _errorHandler.DisplayError("Guest not found. Try again...");
                    continue;
                }
                Console.WriteLine($"Guest name: {currentGuest.Name}");
                Console.WriteLine($"Guest Email: {currentGuest.Email}");
                Console.Write("Enter new Email: ");
                string emailInput = Console.ReadLine();
                if (!_emailValidator.IsValidEmail(emailInput) || string.IsNullOrEmpty(emailInput.Trim()))
                {
                    _errorHandler.DisplayError("Invalid email input. Try again...");
                    continue;
                }

                currentGuest.Email = emailInput;
                _guestRepo.SaveChanges();
                _updateConfirmation.Confirmation($"The new email address {emailInput} " +
                                    $"\nhas been successfully set.");
                break;


            }
            catch (Exception)
            {
                _errorHandler.DisplayError("");
            }
        }
    }
}
