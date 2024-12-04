using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests.Services.Display
{

    public class DisplayActiveGuests : IDisplayAll
    {
        private IDisplayAllDetails _details;

        public DisplayActiveGuests(IDisplayAllDetails displayAllDetails)
        {
            _details = displayAllDetails;
        }

        public void DisplayAll()
        {
            string displayText = "acitve guests";
            string isActive = "true";
            _details.DisplayAll(displayText, isActive);
        }
    }

}
