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
                if (currentGuest == null) continue;

                var isPaid = _boogkingRepo.GetAllItems().First(b => b.GuestId == id).IsPaid;
                if (!isPaid)
                {
                    _errorHandler.DisplayError("The guest has  unpaid invoice." +
                        "\nThe invoice must be paid to proceed with this action.");
                    continue;
                }

                currentGuest.IsActive = false;
                _guestRepo.SaveChanges();
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
