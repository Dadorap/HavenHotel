using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
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
        private readonly ISharedMenu _menu;
        private readonly ICreate _create;
        private readonly Lazy<IMenu> _display;
        private readonly Lazy<IMenu> _update;
        private readonly Lazy<IMenu> _delete;

        public BookingMenu
            (
           ISharedMenu menu,
           [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
           [KeyFilter("CreateBooking")] ICreate create,
           [KeyFilter("DisplayBookingMenu")] Lazy<IMenu> display,
           [KeyFilter("UpdateBookingMenu")] Lazy<IMenu> update,
           [KeyFilter("DeletedBookingMenu")] Lazy<IMenu> delete
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
            var bookingMenuList = new List<string>
            {
                "New Booking",
                "View Booking",
                "Edit Booking",              
                "Delete Booking",
                "Main Menu"
            };


            _menu.DisplayMenu("booking menu", bookingMenuList, _create.Create, _display.Value.DisplayMenu, _update.Value.DisplayMenu, _delete.Value.DisplayMenu, _mainMenu.Value.DisplayMenu);
        }


    }
}
