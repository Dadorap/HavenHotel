using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.Services.Display
{
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

}