using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Interfaces.RoomsInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.Rooms.RoomServices.Update;

public class PriceUpdate : IUpdateRoom
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptFortId;
    private readonly IRepository<Room> _roomRepo;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly Lazy<INavigationHelper> _navigationHelper;


    public PriceUpdate
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
                var id = _promptFortId.GetValidId("room price", "room");
                var currentRoom = _roomRepo.GetItemById(id);
                if (currentRoom == null)
                {
                    _errorHandler.DisplayError("Room not found. Try again...");
                    continue;
                }

                Console.WriteLine($"Current room price/night: {currentRoom.Price:C}");
                Console.WriteLine($"Enter new room price/night (100 - 5000): ");
                string roomPrice = Console.ReadLine().Trim();
                _navigationHelper.Value.ReturnToMenu(roomPrice);

                if (!decimal.TryParse(roomPrice, out decimal price) || price < 100 || price > 5000)
                {
                    _errorHandler.DisplayError($"The input '{roomPrice}' is not a valid price. " +
                        $"\nPlease try again with a correct value.");
                    continue;
                }

                currentRoom.Price = price;
                _roomRepo.Update(currentRoom);

                _updateConfirmation.Confirmation($"The new room price ({currentRoom.Price:C}/night), " +
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
