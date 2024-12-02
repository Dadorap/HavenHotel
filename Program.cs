using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Guests.GuestServices;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using HavenHotel.SeedingData;

namespace HavenHotel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyContainer.Configure();
            var x = container.Resolve<Seed>();
            x.Seedings();
            var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayAllGuests");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayAllBookings");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayAllRooms");

            displayAllRooms.DisplayAll();
          


            //var menu = container.Resolve<IMainMenu>();
            //menu.DisplayMenu();
            //var createGuest = container.ResolveNamed<ICreate>("CreateGuest");
            var createGuest = container.ResolveNamed<ICreate>("CreateRoom");
            //createGuest.Create();







        }
    }
}

