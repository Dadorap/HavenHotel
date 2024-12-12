using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Controllers.GuestController.Display;


public class DisplayActiveGuests : IDisplayAll
{
    private IDisplayAllDetails _details;

    public DisplayActiveGuests(
        [KeyFilter("DisplayGuestsDetails")] IDisplayAllDetails displayAllDetails)
    {
        _details = displayAllDetails;
    }

    public void DisplayAll()
    {
        _details.DisplayAll("acitve guests", "true");
    }
}
