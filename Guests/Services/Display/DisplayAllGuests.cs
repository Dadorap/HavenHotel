using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Rooms.Services.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests.Services.Display;

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
