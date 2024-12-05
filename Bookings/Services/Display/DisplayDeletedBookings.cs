using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Bookings.Services.Display;

public class DisplayDeletedBookings : IDisplayAll
{
    private IDisplayAllDetails _details;

    public DisplayDeletedBookings
    (
        [KeyFilter("DisplayBookingsDetail")] IDisplayAllDetails displayAllDetails
    )
    {
        _details = displayAllDetails;
    }

    public void DisplayAll()
    {
        _details.DisplayAll("deleted bookings", "false");
    }
}
