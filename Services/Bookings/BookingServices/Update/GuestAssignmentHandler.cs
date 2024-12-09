using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.BookingServices.Services.Update;

public class GuestAssignmentHandler : IGuestAssignmentHandler
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Guest> _guestRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly IPromptForBookingId _promptForBookingId;


    public GuestAssignmentHandler(
        IRepository<Guest> guestRepo,
        IRepository<Booking> bookingRepo,
        IRepository<Room> roomRepo,
        Lazy<INavigationHelper> navigationHelper,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        IErrorHandler errorHandler,
        IUserMessages userMessages,
        IPromptForBookingId promptForBookingId

        )
    {

        _guestRepo = guestRepo;
        _bookingRepo = bookingRepo;
        _roomRepo = roomRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _userMessages = userMessages;
        _mainMenu = mainMenu;
        _promptForBookingId = promptForBookingId;
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
                _bookingRepo.SaveChanges();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The name of the new guest is: {newGuest.Name}. " +
                    $"Room number: {guestRoomNum}.");
                Console.ResetColor();
                Console.Write("Press any key to return to menu...");
                Console.ReadKey();
                _mainMenu.Value.DisplayMenu();
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
