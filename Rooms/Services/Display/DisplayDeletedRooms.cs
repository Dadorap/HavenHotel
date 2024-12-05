using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.Services.Delete;

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

