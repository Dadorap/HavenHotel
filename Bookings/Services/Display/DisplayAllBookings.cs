using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Display;

public class DisplayAllBookings : IDisplayAll
{
    private IDisplayAllDetails _details;

    public DisplayAllBookings
    (
        [KeyFilter("DisplayBookingsDetail")] IDisplayAllDetails displayAllDetails)
    {
        _details = displayAllDetails;
    }

    public void DisplayAll()
    {

        _details.DisplayAll("all bookings", "all");
    }
}
