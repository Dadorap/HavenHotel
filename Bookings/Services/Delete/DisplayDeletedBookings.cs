using HavenHotel.Interfaces.DisplayInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Delete
{
    public class DisplayDeletedBookings : IDisplayAll
    {
        private IDisplayAllDetails _details;

        public DisplayDeletedBookings(IDisplayAllDetails displayAllDetails)
        {
            _details = displayAllDetails;
        }

        public void DisplayAll()
        {
            string displayText = "deleted bookings";
            string isActive = "false";
            _details.DisplayAll(displayText, isActive);
        }
    }
}
