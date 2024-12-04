using HavenHotel.Guests;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Linq;

namespace HavenHotel.Bookings.Services.Display
{
    public class DisplayActiveBookings : IDisplayAll
    {
        private IDisplayAllDetails _details;

        public DisplayActiveBookings(IDisplayAllDetails displayAllDetails)
        {
            _details = displayAllDetails;
        }

        public void DisplayAll()
        {
            string displayText = "acitve bookings";
            string isActive = "true";
            _details.DisplayAll(displayText, isActive);
        }
    }
}
