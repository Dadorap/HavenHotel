using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Utilities.BookingsMenus;

public class DisplayBookingMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDisplayAll _displayActiveBookingDetails;
    private readonly IDisplay _displayBookingDetail;
    private readonly IDisplayAll _displayAllBooking;
    private readonly IDisplayAll _displayAllDeleted;


    public DisplayBookingMenu
    (
        ISharedMenu menu,
       [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
       [KeyFilter("DisplayActiveBookings")] IDisplayAll displayActiveBookingDetails,
       [KeyFilter("DisplayBooking")] IDisplay displayBookingDetail,
       [KeyFilter("DisplayAllBookings")] IDisplayAll displayAllBooking,
       [KeyFilter("DisplayDeletedBookings")] IDisplayAll displayAllDeleted)
    {
        _menu = menu;
        _mainMenu = mainMenu;
        _displayActiveBookingDetails = displayActiveBookingDetails;
        _displayBookingDetail = displayBookingDetail;
        _displayAllBooking = displayAllBooking;
        _displayAllDeleted = displayAllDeleted;

    }

    public void DisplayMenu()
    {
        var bookingViewMenu = new List<string>
        {
            "View All Bookings",
            "View Booking Details",
            "View Active Bookings",
            "View Deleted Bookings",
            "Return to Main Menu"
        };

        _menu.DisplayMenu(
            "Booking View Menu",
            bookingViewMenu,
            _displayAllBooking.DisplayAll,
            _displayBookingDetail.DisplayById,
            _displayActiveBookingDetails.DisplayAll,
            _displayAllDeleted.DisplayAll,
            _mainMenu.Value.DisplayMenu
            );

    }
}
