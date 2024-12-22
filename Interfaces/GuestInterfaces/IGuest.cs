namespace HavenHotel.Interfaces.GuestInterfaces;

public interface IGuest
{
    int Id { get; set; }
    string Name { get; set; }
    string PhoneNumber { get; set; }
    string Email { get; set; }
    bool IsActive { get; set; }
}
