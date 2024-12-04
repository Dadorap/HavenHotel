using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings
{
    class BookingMenu : IMenu
    {
        private readonly Lazy<IMenu> _mainMenu;
        private readonly IMainMenu _menu;
        public BookingMenu([KeyFilter("MainMenu")] Lazy<IMenu> mainMenu, IMainMenu menu)
        {
            _mainMenu = mainMenu;
            _menu = menu;
        }
        public void DisplayMenu()
        {
            var bookingMenuList = new List<string>
            {
                "Create New Booking",
                "Show All Bookings",
                "Show One booking",
                "Update a Booking",
                "Delete a Booking",
                "Show All Deleted Bookings",
                "Un-delete a Booking",
                "Back to Main Menu"
            };

            _menu.DisplayMenu("booking menu",bookingMenuList, _mainMenu.Value.DisplayMenu);
        }


    }
}
