using Autofac;
using HavenHotel.Autofac;
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
            var dbContext = scope.Resolve<HotelDbContext>();
            dbContext.MigrateDatabase();
            Seed.Seedings();
            var menu = scope.ResolveNamed<IMenu>("MainMenu");
            var roomAvailability = scope.Resolve<RoomAvailability>();
            roomAvailability.CheckAvailability();
            menu.DisplayMenu();
        }
    }
}
