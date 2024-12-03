using Autofac.Features.AttributeFilters;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Delete
{
    public class DeleteBooking : IDelete
    {
            private readonly IHardDeleteItem _hardDeleteItem;

        public DeleteBooking(IHardDeleteItem hardDeleteItem)
        {
            _hardDeleteItem = hardDeleteItem;
        }
        public void Delete()
        {
            _hardDeleteItem.HardDelete("booking");
            
        }
    }
}
