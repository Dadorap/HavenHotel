using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Interfaces.RoomsInterfaces;
using HavenHotel.Models;
using HavenHotel.Utilities;

namespace HavenHotel.Services.Rooms.RoomServices.Update;

public class SizeUpdate : IUpdateRoom
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptFortId;
    private readonly IRepository<Room> _roomRepo;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly Lazy<INavigationHelper> _navigationHelper;


    public SizeUpdate
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
                var id = _promptFortId.GetValidId("room size", "room");
                var currentRoom = _roomRepo.GetItemById(id);
                if (currentRoom == null)
                {
                    _errorHandler.DisplayError("Room not found. Try again...");
                    continue;
                }

                (int minSize, int maxSize) = currentRoom.RoomType switch
                {
                    RoomType.SINGLE => (12, 15),
                    RoomType.DOUBLE => (15, 25),
                    RoomType.SUITE => (25, 30),
                    RoomType.FAMILY => (30, 50),
                    _ => throw new ArgumentException("Invalid room type")
                };

                Console.WriteLine($"Current room size: {currentRoom.Size}");
                Console.Write($"Enter room size ({minSize}m²-{maxSize}m²): ");
                string roomSize = Console.ReadLine().Trim();
                _navigationHelper.Value.ReturnToMenu(roomSize);

                if (!int.TryParse(roomSize, out int size) || size < minSize || size > maxSize)
                {
                    _errorHandler.DisplayError($"Invalid room size. Please enter a value between {minSize} and {maxSize}.");
                    continue;
                }

                currentRoom.Size = size;
                _roomRepo.Update(currentRoom);

                _updateConfirmation.Confirmation($"The new room size ({size}m²), " +
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
