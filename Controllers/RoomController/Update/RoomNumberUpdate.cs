using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Interfaces.RoomsInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.Rooms.RoomServices.Update;

public class RoomNumberUpdate : IUpdateRoom
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptFortId;
    private readonly IRepository<Room> _roomRepo;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly Lazy<INavigationHelper> _navigationHelper;


    public RoomNumberUpdate
        (
        IErrorHandler errorHandler,
        IPromptForId promptForId,
        IRepository<Room> roomRepo,
        IUpdateConfirmation updateConfirmation,
        Lazy<INavigationHelper> navigationHelper
        )
    {
        _errorHandler = errorHandler;
        _promptFortId = promptForId;
        _roomRepo = roomRepo;
        _updateConfirmation = updateConfirmation;
        _navigationHelper = navigationHelper;
    }

   public void UpdateRoom()
{
    while (true)
    {
        try
        {
            var id = _promptFortId.GetValidId("room number", "room");
            var currentRoom = _roomRepo.GetItemById(id);
            if (currentRoom == null)
            {
                _errorHandler.DisplayError("Room not found. Try again...");
                continue;
            }

            Console.WriteLine($"Current room number: {currentRoom.RoomNumber}");
            Console.Write("Enter new room number(100-500): ");
            string roomNumber = Console.ReadLine().Trim();
            _navigationHelper.Value.ReturnToMenu(roomNumber);

            if (!int.TryParse(roomNumber, out int newRoomNum))
            {
                _errorHandler.DisplayError("Invalid room number input. Try again...");
                continue;
            }
            if (newRoomNum < 100 || newRoomNum > 500)
            {
               _errorHandler.DisplayError("Room number must be between 100 and 500. Try again...");
               continue;
            }

                if (_roomRepo.GetAllItems().Any(r => r.RoomNumber == newRoomNum))
            {
                _errorHandler.DisplayError("Room number already exists. Try again...");
                continue;
            }

            currentRoom.RoomNumber = newRoomNum;
            _roomRepo.Update(currentRoom);
            _updateConfirmation.Confirmation($"The new room number {roomNumber}, " +
                                             "\nhas been successfully set.");
            break;
        }
        catch (FormatException ex)
        {
            _errorHandler.DisplayError($"Input error: {ex.Message}");
        }
        catch (Exception ex)
        {
            _errorHandler.DisplayError(ex.Message);
        }
    }
}

}
