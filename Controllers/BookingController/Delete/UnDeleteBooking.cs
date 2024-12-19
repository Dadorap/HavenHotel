using HavenHotel.Interfaces.DeleteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Controllers.BookingController.Delete
{
    public class UnDeleteBooking : IUnDelete
    {
        private readonly IUnDeleteItem _unDeleteItem;
        public UnDeleteBooking(IUnDeleteItem unDeleteItem)
        {
            _unDeleteItem = unDeleteItem;
        }
        public void UndoDete()
        {
            _unDeleteItem.UnDelete("BOOKING");
        }
    }
}
