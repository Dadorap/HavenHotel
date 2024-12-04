using HavenHotel.Interfaces.GuestInterfaces;

namespace HavenHotel.Guests
{
    public class Guest : IGuest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
