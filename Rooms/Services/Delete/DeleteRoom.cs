using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.Services.Delete
{
    public class DeleteRoom : IDelete
    {
        private readonly IHardDeleteItem _hardDeleteItem;

        public DeleteRoom(IHardDeleteItem hardDeleteItem)
        {
            _hardDeleteItem = hardDeleteItem;
        }
        public void Delete()
        {
            _hardDeleteItem.HardDelete("room");

        }
    }
}
