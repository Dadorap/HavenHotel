using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.Services.Display
{
    public class DisplayActiveRooms : IDisplayAll
    {
        private readonly IDisplayAllDetails _displayAllRooms;
        public DisplayActiveRooms(IDisplayAllDetails displayAllRooms)
        {
            _displayAllRooms = displayAllRooms;
        }

        public void DisplayAll()
        {
            string displayText = "Available rooms";
            string isActive = "true";
            _displayAllRooms.DisplayAll(displayText, isActive);
        }
    }

}