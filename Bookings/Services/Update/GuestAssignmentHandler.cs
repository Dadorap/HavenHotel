using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;

namespace HavenHotel.Bookings.Services.Update;

public class GuestAssignmentHandler : IGuestAssignmentHandler
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Guest> _guestRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDateValidator _dateValidator;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly IPromptForBookingId _promptForBookingId;


    public GuestAssignmentHandler(
        IRepository<Guest> guestRepo,
        IRepository<Booking> bookingRepo,
        Lazy<INavigationHelper> navigationHelper,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        IDateValidator dateValidator,
        IErrorHandler errorHandler,
        IUserMessages userMessages,
        IPromptForBookingId promptForBookingId

        )
    {

        _guestRepo = guestRepo;
        _bookingRepo = bookingRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _userMessages = userMessages;
        _mainMenu = mainMenu;
        _dateValidator = dateValidator;
        _promptForBookingId = promptForBookingId;
    }
    public void UpdateGuestAssignment()
    {
        while (true)
        {
            try
            {
                var getId = _promptForBookingId.GetValidBookingId("Assign New Guest", "GuestAssignmentHandler");
                var booking = _bookingRepo.GetItemById(getId);

                if (booking == null)
                {
                    _errorHandler.DisplayError("Booking not found. Try again...");
                    continue;
                }

                var currentGuest = _guestRepo.GetItemById(booking.GuestId);
                Console.WriteLine($"Current guest name: {currentGuest?.Name ?? "No guest assigned"}");

                Console.WriteLine("Enter new guest ID: ");
                string guestIdInput = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(guestIdInput);

                if (!int.TryParse(guestIdInput, out int newGuestId))
                {
                    _errorHandler.DisplayError("Invalid guest ID. Try again...");
                    continue;
                }

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
                Console.WriteLine($"The name of the new guest is: {newGuest.Name}.");
                Console.Write("Press any key to return to menu...");
                Console.ReadKey();
                _mainMenu.Value.DisplayMenu();
                Console.ResetColor();
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
