using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.Services.Display
{
    public class DisplayDeletedRooms : IDisplayAll
    {
        private readonly IDisplayAllDetails _displayAllRooms;
        public DisplayDeletedRooms(IDisplayAllDetails displayAllRooms)
        {
            _displayAllRooms = displayAllRooms;
        }

        public void DisplayAll()
        {
            string displayText = "unAvailable rooms";
            string isActive = "false";
            _displayAllRooms.DisplayAll(displayText, isActive);
        }
    }
}

