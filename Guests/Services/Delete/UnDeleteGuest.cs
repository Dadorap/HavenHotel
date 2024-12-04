using HavenHotel.Interfaces.DeleteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Guests.Services.Delete
{
    public class UnDeleteGuest : IUnDelete
    {
        private readonly IUnDeleteItem _unDeleteItem;
        public UnDeleteGuest(IUnDeleteItem unDeleteItem)
        {
            _unDeleteItem = unDeleteItem;
        }
        public void UndoDete()
        {
            _unDeleteItem.UnDelete("guest");
        }
    }
}
