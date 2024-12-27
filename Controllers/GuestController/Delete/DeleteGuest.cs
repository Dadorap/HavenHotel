using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.GuestController.Delete;

public class DeleteGuest : IDelete
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptForId;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly IRepository<Guest> _guestRepo;
    private readonly IRepository<Booking> _boogkingRepo;

    public DeleteGuest
        (
        IErrorHandler errorHandler,
        IPromptForId promptForId,
        IUpdateConfirmation updateConfirmation,
        IRepository<Guest> guestRepo,
        IRepository<Booking> boogkingRepo
        )
    {
        _errorHandler = errorHandler;
        _promptForId = promptForId;
        _updateConfirmation = updateConfirmation;
        _guestRepo = guestRepo;
        _boogkingRepo = boogkingRepo;
    }
    public void Delete()
    {
        while (true)
        {
            try
            {
                var id = _promptForId.GetValidId("hard delete", "guest");
                var currentGuest = _guestRepo.GetItemById(id);
                if (currentGuest == null) continue;

                var booking = _boogkingRepo.GetAllItems().FirstOrDefault(b => b.GuestId == id);
                if (booking != null && !booking.IsPaid)
                {
                    _errorHandler.DisplayError("The guest has  unpaid invoice." +
                        "\nThe invoice must be paid to proceed with this action.");
                    continue;
                }

                _guestRepo.RemoveItemById(id);
                _updateConfirmation.Confirmation($"Guest with ID: {currentGuest.Id}. " +
                    $"\nHas been deleted successfully.");

            }
            catch (Exception)
            {
                _errorHandler.DisplayError("Invalid input. Try again.");
            }
        }
    }
}
