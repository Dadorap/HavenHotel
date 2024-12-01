using HavenHotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests
{
    public class GuestMenu
    {
        private readonly IMenu _menu;
        public GuestMenu(IMenu menu)
        {
            _menu = menu;
        }
        public void GuestsMenu()
        {
            var guestMenu = new List<string>
            {
                "Create New Guest",
                "Show All Guests",
                "Update a Guest",
                "Delete a Guest",
                "Show All Deleted Guests",
                "Un-delete a Guest",
                "Back to Main Menu"
            };



        }
    }
}
