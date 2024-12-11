using HavenHotel.Models;
using Microsoft.EntityFrameworkCore;


namespace HavenHotel.Data;

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
