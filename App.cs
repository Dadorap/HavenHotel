using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Data.SeedingData;
using HavenHotel.Interfaces;

namespace HavenHotel;

public static class App
{
    public static void Run()
    {
        var container = DependencyContainer.Configure();
        Seed.Seedings();
        var menu = container.ResolveNamed<IMenu>("MainMenu");
        menu.DisplayMenu();

    }
}
