using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Guests.GuestServices;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using HavenHotel.Rooms.RoomServices;
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
            //var create = container.ResolveNamed<ICreate>("CreateRoom");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayAllGuests");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayAllBookings");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayAllRooms");
            //var displayAllRooms = container.ResolveNamed<IDisplay>("DisplayRoomDetails");
            //var displayAllRooms = container.ResolveNamed<IDisplay>("DisplayGuestDetails");
            var displayAllRooms = container.ResolveNamed<IDisplay>("DisplayBookingDetails");
            //var delete = container.ResolveNamed<IDelete>("DeleteBooking");

            //create.Create();
            //displayAllRooms.DisplayAll();
            displayAllRooms.DisplayById();
            //delete.Delete();



            //var menu = container.Resolve<IMainMenu>();
            //menu.DisplayMenu();
            //var createGuest = container.ResolveNamed<ICreate>("CreateGuest");
            //var createGuest = container.ResolveNamed<ICreate>("CreateRoom");
            //createGuest.Create();







        }
    }
}

