using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Interfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.Rooms.RoomServices.Update;

public class RoomNumberUpdate : IRoomNumberUpdate
{
    private readonly IEmailValidator _emailValidator;
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptFortId;
    private readonly IRepository<Room> _roomRepo;
    private readonly IUpdateConfirmation _updateConfirmation;
    private readonly Lazy<INavigationHelper> _navigationHelper;


    public RoomNumberUpdate
        (
        IEmailValidator emailValidator,
        IErrorHandler errorHandler,
        IPromptForId promptForId,
        IRepository<Room> roomRepo,
        IUpdateConfirmation updateConfirmation,
        Lazy<INavigationHelper> navigationHelper
        )
    {
        _emailValidator = emailValidator;
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
                var id = _promptFortId.GetValidId("room", "room");
                var currentRoom = _roomRepo.GetItemById(id);
                if (currentRoom == null)
                {
                    _errorHandler.DisplayError("Guest not found. Try again...");
                    continue;
                }
                Console.WriteLine($"Current room number: {currentRoom.RoomNumber}");
                Console.Write("Enter new room number: ");
                string roomNumber = Console.ReadLine().Trim();
                _navigationHelper.Value.ReturnToMenu(roomNumber);
                if (int.TryParse(roomNumber, out int roomNum) || !_emailValidator.IsValidEmail(roomNumber) || string.IsNullOrEmpty(roomNumber) )
                {
                    _errorHandler.DisplayError("Invalid email input. Try again...");
                    continue;
                }

                currentRoom.RoomNumber = roomNum;
                _roomRepo.SaveChanges();
                _updateConfirmation.Confirmation($"The new room number {roomNumber} " +
                                    $"\nhas been successfully set.");
                break;


            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError(ex.Message);
            }
        }
    }
}
