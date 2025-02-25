﻿using HavenHotel.Interfaces.BookingInterfaces;

namespace HavenHotel.Models
{
    public class Booking : IBooking
    {

        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }


        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public bool IsPaid { get; set; } = false;
        public bool IsActive { get; set; } = true;


    }

}
