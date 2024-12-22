using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Utilities.RoomsMenus;

public class DisplayRoomMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDisplayAll _displayAvailableRooms;
    private readonly IDisplay _displayRoomDetail;
    private readonly IDisplayAll _displayAllRooms;
    private readonly IDisplayAll _displayUnavailableRooms;

    public DisplayRoomMenu(
        ISharedMenu sharedMenu,
       [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
       [KeyFilter("DisplayActiveRooms")] IDisplayAll displayActiveRoomDetails,
       [KeyFilter("DisplayRoom")] IDisplay displayRoomDetail,
       [KeyFilter("DisplayAllRooms")] IDisplayAll displayAllRoom,
       [KeyFilter("DisplayDeletedRooms")] IDisplayAll displayAllDeleted)
    {
        _menu = sharedMenu;
        _mainMenu = mainMenu;
        _displayAvailableRooms = displayActiveRoomDetails;
        _displayRoomDetail = displayRoomDetail;
        _displayAllRooms = displayAllRoom;
        _displayUnavailableRooms = displayAllDeleted;
    }

    public void DisplayMenu()
    {
        var roomViewMenu = new List<string>
        {
            "View All Rooms",
            "View Room Details",
            "View Available Rooms",
            "View Unavailable Rooms",
            "Return to Main Menu"
        };

        _menu.DisplayMenu(
            "Room View Menu",
            roomViewMenu,
            _displayAllRooms.DisplayAll,
            _displayRoomDetail.DisplayById,
            _displayAvailableRooms.DisplayAll,
            _displayUnavailableRooms.DisplayAll,
            _mainMenu.Value.DisplayMenu
        );

    }
}
