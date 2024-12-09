using HavenHotel.Interfaces.DeleteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Services.RoomServices.Services.Delete
{
    public class UnDeleteRoom : IUnDelete
    {
        private readonly IUnDeleteItem _unDeleteItem;
        public UnDeleteRoom(IUnDeleteItem unDeleteItem)
        {
            _unDeleteItem = unDeleteItem;
        }
        public void UndoDete()
        {
            _unDeleteItem.UnDelete("room");
        }
    }
}
