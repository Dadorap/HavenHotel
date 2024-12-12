using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Controllers.RoomController.Display;

public class DisplayActiveRooms : IDisplayAll
{
    private readonly IDisplayAllDetails _details;
    public DisplayActiveRooms(
       [KeyFilter("DisplayRoomsDetails")] IDisplayAllDetails displayAllRooms)
    {
        _details = displayAllRooms;
    }

    public void DisplayAll()
    {

        _details.DisplayAll("Available rooms", "true");
    }
}