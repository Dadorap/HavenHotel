namespace HavenHotel.Rooms
{
    public class Room
    {
        public int Id { get; set; }
        public decimal Price { get; set; } 
        public RoomType RoomType { get; set; } 
        public int ExtraBed { get; set; } 
        public int Size { get; set; } 
        public int TotalGuests { get; set; } 
        public bool IsAvailable { get; set; } = true;
       
    }

}
