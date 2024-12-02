using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.RoomServices
{
    public class DisplayAllDeletedRooms : IDisplayAll
    {
        private IRepository<Room> _roomsRepository;
        public DisplayAllDeletedRooms(IRepository<Room> repository)
        {
            _roomsRepository = repository;
        }
        public void DisplayAll()
        {
            _roomsRepository.GetAllItems();
        }
    }
}
