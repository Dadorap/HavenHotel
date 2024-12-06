using Autofac.Features.AttributeFilters;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Update;

public class DateRange : IDateRange
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly IRepository<Guest> _guestRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDateValidator _dateValidator;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly IBookingIdRenderer _bookingSidebarDisplay;


    public DateRange(
        IRepository<Guest> guestRepo,
        IRepository<Room> roomRepo,
        IRepository<Booking> bookingRepo,
        Lazy<INavigationHelper> navigationHelper,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        IDateValidator dateValidator,
        IErrorHandler errorHandler,
        IUserMessages userMessages,
        IBookingIdRenderer bookingSidebarDisplay

        )
    {
        _guestRepo = guestRepo;
        _roomRepo = roomRepo;
        _bookingRepo = bookingRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _userMessages = userMessages;
        _bookingSidebarDisplay = bookingSidebarDisplay;
        _mainMenu = mainMenu;
        _dateValidator = dateValidator;
    }

    public void UpdateDate()
    {
        while(true)
        {
            try
            {
                _bookingSidebarDisplay.DisplayBookingNumber("update booking date");

                Console.WriteLine("Enter ");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError(ex.Message);
                continue;
            }
        }
    }
}
