﻿using HavenHotel.BookingsFolder;
using HavenHotel.GuestsFolder;
using HavenHotel.RoomsFolder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  

namespace HavenHotel.Data
{

    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        // Parameterless constructor for design-time tools
        public HotelDbContext() { }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HavenDatabase;Integrated Security=True;TrustServerCertificate=True;");
            }
        }
    }

}
