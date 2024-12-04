using HavenHotel.Interfaces.DisplayInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests.Services.Display
{
    public class DisplayDeletedGuests : IDisplayAll
    {
        private IDisplayAllDetails _details;

        public DisplayDeletedGuests(IDisplayAllDetails displayAllDetails)
        {
            _details = displayAllDetails;
        }

        public void DisplayAll()
        {
            string displayText = "unacitve guests";
            string isActive = "false";
            _details.DisplayAll(displayText, isActive);
        }
    }
}
