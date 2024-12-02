using Autofac;
using HavenHotel.Bookings;
using HavenHotel.Common;
using HavenHotel.Interfaces;
using HavenHotel.Menus;
using HavenHotel.Rooms;
using HavenHotel.Data;
using System;
using HavenHotel.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HavenHotel.Guests;
using HavenHotel.SeedingData;
using HavenHotel.Guests.GuestServices;
using HavenHotel.Bookings.BookingServices;
using HavenHotel.Rooms.RoomServices;

namespace HavenHotel.Configuration
{
    public static class DependencyContainer
    {
        public static IContainer Configure()
        {
            var containerBuilder = new ContainerBuilder();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            containerBuilder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

                return new HotelDbContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            
            containerBuilder.RegisterType<Room>().As<IRoom>();
            containerBuilder.RegisterType<Guest>().As<IGuest>();
            containerBuilder.RegisterType<Booking>().As<IBooking>();

            //Create
            containerBuilder.RegisterType<CreateGuest>().Named<ICreate>("CreateGuest");
            containerBuilder.RegisterType<CreateRoom>().Named<ICreate>("CreateRoom");
            containerBuilder.RegisterType<CreateBooking>().Named<ICreate>("CreateBooking");  
            //DisplayAll
            containerBuilder.RegisterType<DisplayAllRooms>().Named<IDisplayAll>("DisplayAllRooms");
            containerBuilder.RegisterType<DisplayAllGuests>().Named<IDisplayAll>("DisplayAllGuests");
            containerBuilder.RegisterType<DisplayAllBookings>().Named<IDisplayAll>("DisplayAllBookings");


            containerBuilder.RegisterType<ErrorHandler>().As<IErrorHandler>();
            containerBuilder.RegisterType<NavigationHelper>().As<INavigationHelper>();




            containerBuilder.RegisterType<RoomMenu>().AsSelf();
            containerBuilder.RegisterType<BookingMenu>().AsSelf();
            containerBuilder.RegisterType<GuestMenu>().AsSelf();
            containerBuilder.RegisterType<Seed>().AsSelf();

            containerBuilder.RegisterType<Exit>().As<IExit>();
            containerBuilder.RegisterType<Header>().As<IHeader>();
            containerBuilder.RegisterType<MainMenu>().As<IMainMenu>();
            containerBuilder.RegisterType<Menu>().As<IMenu>();


            return containerBuilder.Build();
        }
    }
}