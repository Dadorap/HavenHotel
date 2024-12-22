using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Utilities.GuestsMenus;

public class DisplayGuestMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDisplayAll _displayAllAcitve;
    private readonly IDisplay _displayGuestDetail;
    private readonly IDisplayAll _displayAll;
    private readonly IDisplayAll _displayAllDeleted;

    public DisplayGuestMenu(
        ISharedMenu sharedMenu,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        [KeyFilter("DisplayActiveGuests")] IDisplayAll displayAllAcitve,
        [KeyFilter("DisplayGuest")] IDisplay displayGuestDetail,
        [KeyFilter("DisplayAllGuests")] IDisplayAll displayAll,
        [KeyFilter("DisplayDeletedGuests")] IDisplayAll displayAllDeleted)
    {
        _menu = sharedMenu;
        _mainMenu = mainMenu;
        _displayAllAcitve = displayAllAcitve;
        _displayGuestDetail = displayGuestDetail;
        _displayAll = displayAll;
        _displayAllDeleted = displayAllDeleted;
    }

    public void DisplayMenu()
    {
        var guestViewMenu = new List<string>
{
    "View All Guests",
    "View Guest Details",
    "View Active Guests",
    "View Deleted Guests",
    "Return to Main Menu"
};

        _menu.DisplayMenu(
            "Guest View Menu",
            guestViewMenu,
            _displayAll.DisplayAll,
            _displayGuestDetail.DisplayById,
            _displayAllAcitve.DisplayAll,
            _displayAllDeleted.DisplayAll,
            _mainMenu.Value.DisplayMenu
        );
    }

}
