using HavenHotel.Interfaces.DeleteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Controllers.RoomController.Delete
{
    public class SoftDeleteRoom : ISoftDelete
    {
        private readonly ISoftDeleteItem _item;
        public SoftDeleteRoom(ISoftDeleteItem softDeleteItem)
        {
            _item = softDeleteItem;
        }
        public void SoftDelete()
        {
            _item.SoftDelete("room");
        }
    }
}
