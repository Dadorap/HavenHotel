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
        private readonly IMainMenu _menu;
        public GuestMenu([KeyFilter("MainMenu")] Lazy<IMenu> mainMenu, IMainMenu menu)
        {
            _mainMenu = mainMenu;
            _menu = menu;
        }
        public void DisplayMenu()
        {
            var guestMenuList = new List<string>
            {
                "Create New Guest",
                "Show All Guests",
                "Update a Guest",
                "Delete a Guest",
                "Show All Deleted Guests",
                "Un-delete a Guest",
                "Back to Main Menu"
            };

            _menu.DisplayMenu("guest menu",guestMenuList, _mainMenu.Value.DisplayMenu);
        }
    }
}

