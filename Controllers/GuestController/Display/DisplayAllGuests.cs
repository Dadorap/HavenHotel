using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Controllers.GuestController.Display;

public class DisplayAllGuests : IDisplayAll
{
    private IDisplayAllDetails _details;

    public DisplayAllGuests(
        [KeyFilter("DisplayGuestsDetails")] IDisplayAllDetails details)
    {
        _details = details;
    }

    public void DisplayAll()
    {
        _details.DisplayAll("Available rooms", "all");

    }
}
