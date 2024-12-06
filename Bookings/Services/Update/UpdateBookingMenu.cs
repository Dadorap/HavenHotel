using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Update
{
    public class UpdateBookingMenu : IMenu
    {
        private readonly ISharedMenu _menu;
        private readonly Lazy<IMenu> _mainMenu;



        public UpdateBookingMenu
        (
            ISharedMenu menu,
           [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
           Lazy<IMenu> mainMenu2
        )

        {
            _menu = menu;
            _mainMenu = mainMenu;


        }

        public void DisplayMenu()
        {
            var bookingViewMenu = new List<string>
        {
            "View All Bookings",
            "View Booking Details",
            "View Active Bookings",
            "View Deleted Bookings",
            "Return to Main Menu"
        };

            _menu.DisplayMenu(
                "Booking View Menu",
                bookingViewMenu,
                _mainMenu.Value.DisplayMenu
                );

        }
    }
}
