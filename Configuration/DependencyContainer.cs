using Autofac;
using HavenHotel.BookingsFolder;
using HavenHotel.ExitFolder;
using HavenHotel.HeaderFolder;
using HavenHotel.InterfaceFolder;
using HavenHotel.Interfaces;
using HavenHotel.MenuFolder;
using HavenHotel.RoomsFolder;
using HavenHotel.Data; 
using System;
using HavenHotel.Repositories;

namespace HavenHotel.Configuration
{
    public static class DependencyContainer
    {
        public static IContainer Configure()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<RoomMenu>().AsSelf();
            containerBuilder.RegisterType<BookingMenu>().AsSelf();
            containerBuilder.RegisterType<RoomMenu>().AsSelf();

            containerBuilder.RegisterType<Exit>().As<IExit>();
            containerBuilder.RegisterType<Header>().As<IHeader>();
            containerBuilder.RegisterType<MainMenu>().As<IMainMenu>();
            containerBuilder.RegisterType<Menu>().As<IMenu>();

            containerBuilder.RegisterType<HotelDbContext>().AsSelf().InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            return containerBuilder.Build();
        }
    }
}
