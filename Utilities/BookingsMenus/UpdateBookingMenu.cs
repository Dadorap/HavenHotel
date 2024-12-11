using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;

namespace HavenHotel.Utilities.BookingsMenus;

public class UpdateBookingMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDateRange _dateRange;
    private readonly IGuestAssignmentHandler _guestAssignmentHandler;



    public UpdateBookingMenu
    (
        ISharedMenu menu,
       [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
       IDateRange dateRange,
       IGuestAssignmentHandler guestAssignmentHandler

    )

    {
        _menu = menu;
        _mainMenu = mainMenu;
        _dateRange = dateRange;
        _guestAssignmentHandler = guestAssignmentHandler;

    }

    public void DisplayMenu()
    {
        var updateMenu = new List<string>
        {
            "Assign New Guest",
            "Update Booking Dates",
            "Change Assigned Room",
            "Recalculate Total Price",
            "Back to Main Menu"
        };


        _menu.DisplayMenu(
            "Booking View Menu",
            updateMenu,
            _guestAssignmentHandler.UpdateGuestAssignment,
            _dateRange.UpdateDate,
            _mainMenu.Value.DisplayMenu
            );

    }
}
