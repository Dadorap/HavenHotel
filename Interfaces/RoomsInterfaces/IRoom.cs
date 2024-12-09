using HavenHotel.Services.RoomServices;

namespace HavenHotel.Interfaces.RoomsInterfaces
{
    public interface IRoom
    {
        int ExtraBed { get; set; }
        int Id { get; set; }
        bool IsActive { get; set; }
        decimal Price { get; set; }
        RoomType RoomType { get; set; }
        int Size { get; set; }
        int TotalGuests { get; set; }
    }
}