using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.RoomsInterfaces;
using HavenHotel.Models;
using HavenHotel.Utilities;

namespace HavenHotel.Services.Rooms.RoomServices.Update;

public class RoomTypeUpdate : IUpdateRoom
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptFortId;
    private readonly IRepository<Room> _roomRepo;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly Lazy<INavigationHelper> _navigationHelper;


    public RoomTypeUpdate
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
                var id = _promptFortId.GetValidId("room type", "room");
                var currentRoom = _roomRepo.GetItemById(id);
                if (currentRoom == null)
                {
                    _errorHandler.DisplayError("Room not found. Try again...");
                    continue;
                }

                Console.WriteLine($"Current room type: {currentRoom.RoomType}");
                Console.Write($"Enter new room type: ");
                string roomTypeInput = Console.ReadLine().Trim();
                _navigationHelper.Value.ReturnToMenu(roomTypeInput);
                if (!Enum.TryParse(roomTypeInput, true, out RoomType roomType)
                    || !Enum.IsDefined(typeof(RoomType), roomType))
                {
                    _errorHandler.DisplayError($"The input '{roomTypeInput}' is not a valid. " +
                        $"\nPlease try again with a correct room type."); 
                    continue;
                }

                currentRoom.RoomType = roomType;
                _roomRepo.Update(currentRoom);
                _updateConfirmation.Confirmation($"The new room type: ({currentRoom.RoomType}), " +
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
