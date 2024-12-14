using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Data;
using HavenHotel.Data.SeedingData;
using HavenHotel.Interfaces;
using HavenHotel.Utilities;

namespace HavenHotel;

public static class App
{
    public static void Run()
    {
        var container = DependencyContainer.Configure();

        using (var scope = container.BeginLifetimeScope())
        {
            var menu = scope.ResolveNamed<IMenu>("MainMenu");
            var roomAvailability = scope.Resolve<RoomAvailability>();
            var dbContext = scope.Resolve<HotelDbContext>();
            dbContext.MigrateDatabase();
            Seed.Seedings();
            roomAvailability.CheckAvailability();
            menu.DisplayMenu();
        }
    }
}
