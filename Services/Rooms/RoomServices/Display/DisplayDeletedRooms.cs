using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Services.RoomServices.Services.Display;

public class DisplayDeletedRooms : IDisplayAll
{
    private readonly IDisplayAllDetails _displayAllRooms;
    public DisplayDeletedRooms(
        [KeyFilter("DisplayRoomsDetails")] IDisplayAllDetails displayAllRooms)
    {
        _displayAllRooms = displayAllRooms;
    }

    public void DisplayAll()
    {
        _displayAllRooms.DisplayAll("unAvailable rooms", "false");
    }
}

