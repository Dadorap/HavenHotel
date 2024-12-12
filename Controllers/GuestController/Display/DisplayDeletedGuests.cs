using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Controllers.GuestController.Display;

public class DisplayDeletedGuests : IDisplayAll
{
    private IDisplayAllDetails _details;

    public DisplayDeletedGuests(
        [KeyFilter("DisplayGuestsDetails")] IDisplayAllDetails displayAllDetails)
    {
        _details = displayAllDetails;
    }

    public void DisplayAll()
    {
        _details.DisplayAll("Inactive Guests", "false");
    }
}
