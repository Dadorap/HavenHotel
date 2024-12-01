using HavenHotel.GuestsFolder;
using HavenHotel.RoomsFolder;

namespace HavenHotel.BookingsFolder
{
    public class Booking
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Room RoomID { get; set; }
        public Guest GuestID { get; set; }
        public decimal Price { get; set; }
    }
}
