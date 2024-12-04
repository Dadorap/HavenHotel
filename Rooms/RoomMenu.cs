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
    private readonly Lazy<IMenuMain> _mainMenu;
    private readonly IMainMenu _menu;
    public RoomMenu(Lazy<IMenuMain> mainMenu, IMainMenu menu)
    {
        _mainMenu = mainMenu;
        _menu = menu;
    }


    public void DisplayMenu()
    {
        var roomsMenuList = new List<string>
        {
            "Create New Room",
            "Show All Rooms",
            "Update a Room",
            "Delete a Room",
            "Show All Deleted Rooms",
            "Un-delete a Room",
            "Back to Main Menu"
        };

        _menu.DisplayMenu("room menu",roomsMenuList, _mainMenu.Value.DisplayMenu);


    }
}
