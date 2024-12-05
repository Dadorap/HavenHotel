using HavenHotel.Interfaces.DisplayInterfaces;

namespace HavenHotel.Guests.Services.Display;


public class DisplayActiveGuests : IDisplayAll
{
    private IDisplayAllDetails _details;

    public DisplayActiveGuests(IDisplayAllDetails displayAllDetails)
    {
        _details = displayAllDetails;
    }

    public void DisplayAll()
    {
        _details.DisplayAll("acitve guests", "true");
    }
}
