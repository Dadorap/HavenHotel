using HavenHotel.Rooms;

namespace HavenHotel.Interfaces
{
    public interface IRoom
    {
        int ExtraBed { get; set; }
        int Id { get; set; }
        bool IsAvailable { get; set; }
        decimal Price { get; set; }
        RoomType RoomType { get; set; }
        int Size { get; set; }
        int TotalGuests { get; set; }
    }
}