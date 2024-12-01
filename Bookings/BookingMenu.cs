using HavenHotel.InterfaceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.BookingsFolder
{
    class BookingMenu
    {

        private readonly IMenu _menu;
        public BookingMenu(IMenu menu)
        {
            _menu = menu;
        }
        public void BookingsMenu()
        {
            var guestOperations = new List<string>
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



        }
    }
}
