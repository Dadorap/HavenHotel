using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms;

public class RoomMenu : IMenu
{
    private readonly Lazy<IMenu> _mainMenu;
    private readonly ISharedMenu _menu;
    public RoomMenu([KeyFilter("MainMenu")] Lazy<IMenu> mainMenu, ISharedMenu menu)
    {
        _mainMenu = mainMenu;
        _menu = menu;
    }


    public void DisplayMenu()
    {
        var roomMenuList = new List<string>
        {
            "New Room",
            "View Room",
            "Edit Room",
            "View Rooms",
            "Delete Room",
            "View Deleted",
            "Restore Room",
            "Main Menu"
        };


        _menu.DisplayMenu("room menu", roomMenuList, _mainMenu.Value.DisplayMenu);


    }
}
