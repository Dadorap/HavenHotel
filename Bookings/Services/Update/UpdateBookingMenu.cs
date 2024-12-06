using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
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
           [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu
        )

        {
            _menu = menu;
            _mainMenu = mainMenu;


        }

        public void DisplayMenu()
        {
            var updateMenu = new List<string>
            {
                "Assign New Guest",
                "Update Booking Dates",
                "Change Assigned Room",
                "Recalculate Total Price",
                "Back to Main Menu"
            };


            _menu.DisplayMenu(
                "Booking View Menu",
                updateMenu,
                _mainMenu.Value.DisplayMenu
                );

        }
    }
}
