using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Data;
using HavenHotel.Data.SeedingData;
using HavenHotel.Interfaces;

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
            menu.DisplayMenu();
        }
    }
}
