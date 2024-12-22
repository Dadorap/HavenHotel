using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;

namespace HavenHotel.Utilities.RoomsMenus;

public class RoomMenu : IMenu
{
    private readonly Lazy<IMenu> _mainMenu;
    private readonly ISharedMenu _menu;
    private readonly ICreate _create;
    private readonly Lazy<IMenu> _display;
    private readonly Lazy<IMenu> _update;
    private readonly Lazy<IMenu> _delete;

    public RoomMenu
        (
       ISharedMenu menu,
       [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
       [KeyFilter("CreateRoom")] ICreate create,
       [KeyFilter("DisplayRoomMenu")] Lazy<IMenu> display,
       [KeyFilter("UpdateRoomMenu")] Lazy<IMenu> update,
       [KeyFilter("DeletedRoomMenu")] Lazy<IMenu> delete
        )
    {
        _mainMenu = mainMenu;
        _menu = menu;
        _create = create;
        _display = display;
        _update = update;
        _delete = delete;
    }


    public void DisplayMenu()
    {
        var roomMenuList = new List<string>
        {
            "New Room",
            "View Room",
            "Edit Room",
            "Delete Room",
            "Main Menu"
        };


        _menu.DisplayMenu(
            "room menu",
            roomMenuList,
            _create.Create,
            _display.Value.DisplayMenu,
            _update.Value.DisplayMenu,
            _delete.Value.DisplayMenu,
            _mainMenu.Value.DisplayMenu
            );


    }
}
