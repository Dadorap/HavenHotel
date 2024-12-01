﻿using HavenHotel.Guests;
using HavenHotel.Rooms;

namespace HavenHotel.Bookings
{
    public class Booking
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int RoomId { get; set; } 
        public Room Room { get; set; } 

        
        public int GuestId { get; set; } 
        public Guest Guest { get; set; } 

        public decimal Price { get; set; }
    }

}
