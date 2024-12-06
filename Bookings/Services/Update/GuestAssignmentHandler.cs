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
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly IBookingIdRenderer _bookingSidebarDisplay;


    public GuestAssignmentHandler(
        IRepository<Guest> roomRepo,
        IRepository<Booking> bookingRepo,
        Lazy<INavigationHelper> navigationHelper,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        IErrorHandler errorHandler,
        IUserMessages userMessages,
        IBookingIdRenderer bookingSidebarDisplay

        )
    {

        _guestRepo = roomRepo;
        _bookingRepo = bookingRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _userMessages = userMessages;
        _bookingSidebarDisplay = bookingSidebarDisplay;
        _mainMenu = mainMenu;
    }
    public void UpdateGuestAssignment()
    {
        
    }
}
