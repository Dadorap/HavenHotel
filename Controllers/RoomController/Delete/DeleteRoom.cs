using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.RoomController.Delete;

public class DeleteRoom : IDelete
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptForId;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly IRepository<Room> _roomRepo;

    public DeleteRoom
        (
        IErrorHandler errorHandler, 
        IPromptForId promptForId, 
        IUpdateConfirmation updateConfirmation,
        IRepository<Room> roomRepo
        )
    {
        _errorHandler = errorHandler;
        _promptForId = promptForId;
        _updateConfirmation = updateConfirmation;
        _roomRepo = roomRepo;
    }

    public void Delete()
    {
        //_hardDeleteItem.HardDelete("room");

        while (true)
        {
            try
            {
                var id = _promptForId.GetValidId("hard delete", "room");
                var currentGuest = _roomRepo.GetItemById(id);
                if (currentGuest == null) continue;
                //var isIdFound =
                _roomRepo.RemoveItemById(id);

                _updateConfirmation.Confirmation($"Room with ID: {currentGuest.Id}. " +
                    $"\nHas been deleted successfully.");
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError(ex.Message);
            }
        }

    }
}
