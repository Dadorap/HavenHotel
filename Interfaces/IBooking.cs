using HavenHotel.Guests;
using HavenHotel.Rooms;

namespace HavenHotel.Interfaces
{
    public interface IBooking
    {
        int Id { get; set; }
        DateOnly StartDate { get; set; }
        DateOnly EndDate { get; set; }

        int RoomId { get; set; }
        Room Room { get; set; }


        int GuestId { get; set; }
        Guest Guest { get; set; }
    }
}