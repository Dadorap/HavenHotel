using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests.Services.Delete
{
    public class DeleteGuest : IDelete
    {
        private readonly IHardDeleteItem _hardDeleteItem;

        public DeleteGuest(IHardDeleteItem hardDeleteItem)
        {
            _hardDeleteItem = hardDeleteItem;
        }
        public void Delete()
        {
            _hardDeleteItem.HardDelete("guest");

        }
    }
}
