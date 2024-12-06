using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;

namespace HavenHotel.Bookings.Services.Update;

public class UpdateBookingMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDateRange _dateRange;



    public UpdateBookingMenu
    (
        ISharedMenu menu,
       [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
       IDateRange dateRange
    )

    {
        _menu = menu;
        _mainMenu = mainMenu;
        _dateRange = dateRange;


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
            _dateRange.UpdateDate,
            _mainMenu.Value.DisplayMenu
            );

    }
}
