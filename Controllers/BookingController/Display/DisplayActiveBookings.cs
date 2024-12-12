using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Controllers.BookingController.Display;

public class DisplayActiveBookings : IDisplayAll
{
    private IDisplayAllDetails _details;

    public DisplayActiveBookings
    (
        [KeyFilter("DisplayBookingsDetail")] IDisplayAllDetails displayAllDetails)
    {
        _details = displayAllDetails;
    }

    public void DisplayAll()
    {

        _details.DisplayAll("acitve bookings", "true");
    }
}
