using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.RoomsInterfaces;
using HavenHotel.Models;
using HavenHotel.Utilities;

namespace HavenHotel.Services.Rooms.RoomServices.Update;

public class ExtraBedUpdate : IUpdateRoom
{
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptFortId;
    private readonly IRepository<Room> _roomRepo;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly Lazy<INavigationHelper> _navigationHelper;


    public ExtraBedUpdate
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
                var id = _promptFortId.GetValidId("extra beds", "room");
                var currentRoom = _roomRepo.GetItemById(id);
                if (currentRoom == null)
                {
                    _errorHandler.DisplayError("Room not found. Try again...");
                    continue;
                }
                var allowExtraBeds = currentRoom.RoomType;

                int maxExtraBeds = currentRoom.Size switch
                {
                    <= 15 => 1,
                    <= 25 => 2,
                    _ => 3
                };

                int extraBeds = 0;
                if (allowExtraBeds != RoomType.SINGLE)
                {
                    Console.WriteLine($"Current extra beds: {currentRoom.ExtraBed}");
                    Console.WriteLine($"Room type: {currentRoom.RoomType}");
                    Console.Write($"Enter the number of extra beds (0-{maxExtraBeds}): ");
                    string extraBed = Console.ReadLine().Trim();
                    _navigationHelper.Value.ReturnToMenu(extraBed);
                    if (!int.TryParse(extraBed, out extraBeds) || extraBeds < 0 || extraBeds > maxExtraBeds)
                    {
                        _errorHandler.DisplayError($"Invalid input for extra beds. Try again...");
                        continue;
                    }

                    currentRoom.ExtraBed = extraBeds;
                    _roomRepo.Update(currentRoom);
                    _updateConfirmation.Confirmation($"The new extra beds ({extraBeds}), " +
                                        $"\nhas been successfully set.");
                    break;
                }
                else
                {
                    _errorHandler.DisplayError("Extra beds are not allowed for Single rooms.");
                    continue;
                }

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
