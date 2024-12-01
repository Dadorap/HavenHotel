using Autofac;
using HavenHotel.BookingsFolder;
using HavenHotel.ExitFolder;
using HavenHotel.HeaderFolder;
using HavenHotel.InterfaceFolder;
using HavenHotel.Interfaces;
using HavenHotel.MenuFolder;
using HavenHotel.RoomsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return containerBuilder.Build();
        }

    }
}
