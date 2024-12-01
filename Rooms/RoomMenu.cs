using HavenHotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms
{
    public class RoomMenu
    {
        private readonly IMenu _menu;
        public RoomMenu(IMenu menu)
        {
            _menu = menu;
        }
        public void RoomsMenu()
        {
            var roomOperations = new List<string>
            {
                "Create New Room",
                "Show All Rooms",
                "Update a Room",
                "Delete a Room",
                "Show All Deleted Rooms",
                "Un-delete a Room",
                "Back to Main Menu"
            };




        }
    }
}
