using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Interfaces;
using HavenHotel.SeedingData;

namespace HavenHotel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyContainer.Configure();

            //var menu = container.Resolve<IMainMenu>();

            //menu.DisplayMenu();

            var x = container.Resolve<Seedings>();


            x.Seed();
        }
    }
}

