using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;

namespace HavenHotel.Utilities.BookingsMenus;

public class UpdateBookingMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDateRangeUpdate _dateRange;
    private readonly IGuestAssignmentUpdate _guestAssignmentUpdate;
    private readonly IPaymentDetailUpdate _paymentDetailUpdate;



    public UpdateBookingMenu
    (
        ISharedMenu menu,
       [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
       IDateRangeUpdate dateRange,
       IGuestAssignmentUpdate guestAssignmentHandler,
       IPaymentDetailUpdate paymentDetailUpdate

    )

    {
        _menu = menu;
        _mainMenu = mainMenu;
        _dateRange = dateRange;
        _guestAssignmentUpdate = guestAssignmentHandler;
        _paymentDetailUpdate = paymentDetailUpdate;

    }

    public void DisplayMenu()
    {
        var updateMenu = new List<string>
        {
            "Update Booking Dates",
            "Reassign to New Guest",
            "Recalculate Total Price",
            "Back to Main Menu"
        };


        _menu.DisplayMenu(
            "Booking View Menu",
            updateMenu,
            _dateRange.UpdateDate,
            _guestAssignmentUpdate.UpdateGuestAssignment,
            _paymentDetailUpdate.PaymentDetailUpdater,
            _mainMenu.Value.DisplayMenu
            );

    }
}
