using HavenHotel.Interfaces.DeleteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Delete
{
    public class SoftDeleteBooking : ISoftDelete
    {
        private readonly ISoftDeleteItem _item;
        public SoftDeleteBooking(ISoftDeleteItem softDeleteItem)
        {
            _item = softDeleteItem;
        }
        public void SoftDelete()
        {
            _item.SoftDelete("booking");
        }
    }
}
