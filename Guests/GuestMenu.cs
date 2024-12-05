using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests
{
    public class GuestMenu : IMenu
    {
        private readonly Lazy<IMenu> _mainMenu;
        private readonly ISharedMenu _menu;
        public GuestMenu([KeyFilter("MainMenu")] Lazy<IMenu> mainMenu, ISharedMenu menu)
        {
            _mainMenu = mainMenu;
            _menu = menu;
        }
        public void DisplayMenu()
        {
            var guestMenuList = new List<string>
            {
                "New Guest",
                "View Guest",
                "Edit Guest",
                "View Guests",
                "Delete Guest",
                "View Deleted",
                "Restore Guest",
                "Main Menu"
            };


            _menu.DisplayMenu("guest menu", guestMenuList, _mainMenu.Value.DisplayMenu);
        }
    }
}

