using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Guests.GuestServices;
using HavenHotel.Interfaces;
using HavenHotel.SeedingData;

namespace HavenHotel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyContainer.Configure();
            var x = container.Resolve<Seedings>();

            //var menu = container.Resolve<IMainMenu>();
            x.Seed();
            //menu.DisplayMenu();
            //var createGuest = container.ResolveNamed<ICreate>("CreateGuest");
            var createGuest = container.ResolveNamed<ICreate>("CreateRoom");
            createGuest.Create();







        }
    }
}

