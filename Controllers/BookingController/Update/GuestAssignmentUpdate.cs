using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.BookingController.Update;

public class GuestAssignmentUpdate : IGuestAssignmentUpdate
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Guest> _guestRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForBookingId _promptForBookingId;
    private readonly IUpdateConfirmation _updateConfirmation;



    public GuestAssignmentUpdate(
        IRepository<Guest> guestRepo,
        IRepository<Booking> bookingRepo,
        IRepository<Room> roomRepo,
        Lazy<INavigationHelper> navigationHelper,
        IErrorHandler errorHandler,
        IPromptForBookingId promptForBookingId,
        IUpdateConfirmation updateConfirmation

        )
    {

        _guestRepo = guestRepo;
        _bookingRepo = bookingRepo;
        _roomRepo = roomRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _promptForBookingId = promptForBookingId;
        _updateConfirmation = updateConfirmation;
    }
    public void UpdateGuestAssignment()
    {
        while (true)
        {
            try
            {
                var getId = _promptForBookingId.GetValidBookingId
                    ("Assign New Guest", "GuestAssignmentHandler");
                var booking = _bookingRepo.GetItemById(getId);

                if (booking == null)
                {
                    _errorHandler.DisplayError("Booking not found. Try again...");
                    continue;
                }

                var currentGuest = _guestRepo.GetItemById(booking.GuestId);
                Console.WriteLine($"Current guest name: " +
                    $"{currentGuest.Name}");
                Console.WriteLine($"With guest ID: {currentGuest.Id}");

                Console.Write("Enter new guest ID: ");
                string guestIdInput = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(guestIdInput);

                if (!int.TryParse(guestIdInput, out int newGuestId))
                {
                    _errorHandler.DisplayError("Invalid guest ID. Try again...");
                    continue;
                }
                var guestRoomNum = _roomRepo.GetItemById(booking.RoomId).RoomNumber;
                var newGuest = _guestRepo.GetItemById(newGuestId);
                if (newGuest == null)
                {
                    _errorHandler.DisplayError("Guest does not exist. Try again...");
                    continue;
                }

                booking.GuestId = newGuestId;
                _bookingRepo.Update(booking);
                _updateConfirmation.Confirmation($"The name of the new guest is: {newGuest.Name}. " +
                    $"Room number: {guestRoomNum}.");
                break;
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError($"An error occurred: {ex.Message}");
                continue;
            }
        }
    }

}
