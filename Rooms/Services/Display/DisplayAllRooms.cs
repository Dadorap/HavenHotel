using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Rooms.Services.Display;

public class DisplayAllRooms : IDisplayAll
{
    private IDisplayAllDetails _details;

    public DisplayAllRooms(IDisplayAllDetails details)
    {
        _details = details;
    }

    public void DisplayAll()
    {
        _details.DisplayAll("all rooms", "none");
    }
}
