using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Rooms.Services.Display;

public class DisplayAllRooms : IDisplayAll
{
    private IDisplayAllDetails _details;

    public DisplayAllRooms(
        [KeyFilter("DisplayRoomsDetails")] IDisplayAllDetails details)
    {
        _details = details;
    }

    public void DisplayAll()
    {
        _details.DisplayAll("all rooms", "all");
    }
}
