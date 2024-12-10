using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Data.SeedingData;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Services.Guests.GuestServices.Update;

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
            //var create = container.ResolveNamed<ICreate>("CreateGuest");
            //var create = container.ResolveNamed<ICreate>("CreateBooking");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayDeletedGuests");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayActiveBookings");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayDeletedBookings");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayActiveRooms");
            //var displayAllRooms = container.ResolveNamed<IDisplayAll>("DisplayDeletedRooms");
            //var displayAllRooms = container.ResolveNamed<IDisplay>("DisplayRoomDetails");
            //var displayAllRooms = container.ResolveNamed<IDisplay>("DisplayGuestDetails");
            //var displayAllRooms = container.ResolveNamed<IDisplay>("DisplayBookingDetails");
            //var delete = container.ResolveNamed<IDelete>("DeleteRoom");
            //var delete = container.ResolveNamed<IDelete>("DeleteGuest");
            //var sdelete = container.ResolveNamed<ISoftDelete>("SoftDeleteGuest");
            //var sdelete = container.ResolveNamed<ISoftDelete>("SoftDeleteBooking");
            //var sdelete = container.ResolveNamed<ISoftDelete>("SoftDeleteRoom");
            //var undelete = container.ResolveNamed<IUnDelete>("UnDeleteRoom");            
            //var sdelete = container.ResolveNamed<ISoftDelete>("SoftDeleteGuest");
            //var undelete = container.ResolveNamed<IUnDelete>("UnDeleteGuest");
            //var update = container.Resolve<IDateRange>();
            //var update = container.Resolve<IGuestAssignmentHandler>();
            //var update = container.Resolve<INameUpdate>();
            //var update = container.Resolve<IEmailUpdate>();
            var update = container.Resolve<IPhoneNumberUpdate>();



            //update.UpdateRoom();
            //update.UpdateTotalPrice();
            //update.NameUpdater();
            //update.EmailUpdater();
            //update.PhoneNumberUpdater();
            //update.UpdateDate();
            //create.Create();
            //displayAllRooms.DisplayAll();
            //displayAllRooms.DisplayById();
            //delete.Delete();
            //sdelete.SoftDelete();
            //undelete.UndoDete();



            //var menu = container.ResolveNamed<IMenu>("DeletedRoomMenu");
            //var menu = container.ResolveNamed<IMenu>("UpdateBookingMenu");
            var menu = container.ResolveNamed<IMenu>("MainMenu");
            //var menu = container.ResolveNamed<IMenu>("DisplayGuestMenu");
            //menu.DisplayMenu();
            //var createGuest = container.ResolveNamed<ICreate>("CreateGuest");
            //var createGuest = container.ResolveNamed<ICreate>("CreateRoom");
            //createGuest.Create();







        }
    }
}

