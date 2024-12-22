using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Controllers.BookingController.Display;

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
