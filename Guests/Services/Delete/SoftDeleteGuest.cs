using HavenHotel.Interfaces.DeleteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests.Services.Delete
{
    public class SoftDeleteGuest : ISoftDelete
    {
        private readonly ISoftDeleteItem _item;
        public SoftDeleteGuest(ISoftDeleteItem softDeleteItem)
        {
            _item = softDeleteItem;
        }
        public void SoftDelete()
        {
            _item.SoftDelete("guest");
        }
    }
}
