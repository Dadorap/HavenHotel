using HavenHotel.Data.Repositories;
using HavenHotel.Models;

namespace HavenHotel.Utilities;

public class RoomAvailability
{
    private readonly IRepository<Room> _roomRepo;
    private readonly IRepository<Booking> _bookingRepo;

    public RoomAvailability
        (
        IRepository<Room> roomRepo,
        IRepository<Booking> bookingRepo
        )
    {
        _roomRepo = roomRepo;
        _bookingRepo = bookingRepo;
    }

    public void CheckAvailability()
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        var booking = _bookingRepo
            .GetAllItems()
            .Where(b => currentDate > b.EndDate).ToList();

        booking.ForEach(b =>
        {
            var room = _roomRepo.GetItemById(b.RoomId);
            room.IsAvailable = true;
            _roomRepo.SaveChanges();
        });

    }

}


