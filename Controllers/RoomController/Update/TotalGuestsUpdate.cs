using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.RoomsInterfaces;
using HavenHotel.Models;
using HavenHotel.Utilities;

namespace HavenHotel.Services.Rooms.RoomServices.Update;

public class TotalGuestsUpdate : IUpdateRoom
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptFortId;
    private readonly IRepository<Room> _roomRepo;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly Lazy<INavigationHelper> _navigationHelper;


    public TotalGuestsUpdate
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
                var id = _promptFortId.GetValidId("total guests", "room");
                var currentRoom = _roomRepo.GetItemById(id);
                if (currentRoom == null)
                {
                    _errorHandler.DisplayError("Room not found. Try again...");
                    continue;
                }
                int maxGuests = currentRoom.RoomType switch
                {
                    RoomType.SINGLE => 1,
                    RoomType.DOUBLE => 2,
                    RoomType.SUITE => 4,
                    RoomType.FAMILY => 6,
                    _ => throw new ArgumentException("Invalid room type")
                };

                int totalCapacity = Math.Min(maxGuests + currentRoom.ExtraBed, currentRoom.Size);
                Console.WriteLine($"How many guests are allowed in the room (1-{totalCapacity}):");
                string guestTotal = Console.ReadLine().Trim();
                _navigationHelper.Value.ReturnToMenu(guestTotal);
                if (!int.TryParse(guestTotal, out int totalGuests) || totalGuests < 1 || totalGuests > totalCapacity)
                {
                    _errorHandler.DisplayError($"Invalid input for total guests. Please enter a value between 1 and {totalCapacity}.");
                    continue;
                }




                //currentRoom.TotalGuests = size;
                //_roomRepo.Update(currentRoom);
                //_roomRepo.SaveChanges();

                //_updateConfirmation.Confirmation($"The new total guests ({size}), " +
                //                    $"\nhas been successfully set.");

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
