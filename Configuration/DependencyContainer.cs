using Autofac;
using HavenHotel.Bookings;
using HavenHotel.ExitFolder;
using HavenHotel.HeaderFolder;
using HavenHotel.Interfaces;
using HavenHotel.Menus;
using HavenHotel.Rooms;
using HavenHotel.Data; 
using System;
using HavenHotel.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HavenHotel.Guests;

namespace HavenHotel.Configuration
{
    public static class DependencyContainer
    {
        public static IContainer Configure()
        {
            var containerBuilder = new ContainerBuilder();

            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Register DbContext with options from configuration
            containerBuilder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

                return new HotelDbContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            containerBuilder.RegisterType<RoomMenu>().AsSelf();
            containerBuilder.RegisterType<BookingMenu>().AsSelf();
            containerBuilder.RegisterType<GuestMenu>().AsSelf();

            containerBuilder.RegisterType<Exit>().As<IExit>();
            containerBuilder.RegisterType<Header>().As<IHeader>();
            containerBuilder.RegisterType<MainMenu>().As<IMainMenu>();
            containerBuilder.RegisterType<Menu>().As<IMenu>();


            return containerBuilder.Build();
        }
    }
}