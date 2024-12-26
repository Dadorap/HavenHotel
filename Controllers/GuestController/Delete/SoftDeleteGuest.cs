using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.GuestController.Delete;

public class SoftDeleteGuest : ISoftDelete
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptForId;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly IRepository<Guest> _guestRepo;
    private readonly IRepository<Booking> _boogkingRepo;

    public SoftDeleteGuest
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

    public void SoftDelete()
    {
        while (true)
        {
            try
            {
                var id = _promptForId.GetValidId("soft delete", "guest");
                var currentGuest = _guestRepo.GetItemById(id);

                if (currentGuest == null)
                {
                    _errorHandler.DisplayError($"No guest found with ID: {id}.");
                    continue;
                }

                var booking = _boogkingRepo.GetAllItems().FirstOrDefault(b => b.GuestId == id);
                if (booking != null && !booking.IsPaid)
                {
                    _errorHandler.DisplayError("The guest has an unpaid invoice. Please resolve this issue to proceed.");
                    continue;
                }

                currentGuest.IsActive = false;
                _guestRepo.SaveChanges();
                _updateConfirmation.Confirmation($"Guest with ID: {currentGuest.Id} has been soft-deleted successfully.");
                break;
            }
            catch (Exception)
            {
                _errorHandler.DisplayError("An error occurred. Please try again.");
            }
        }
    }

}
