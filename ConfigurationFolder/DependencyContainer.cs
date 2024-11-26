using Autofac;
using HavenHotel.InterfaceFolder;
using HavenHotel.Interfaces;
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


            containerBuilder.RegisterType<Header>().As<IHeader>();           
            containerBuilder.RegisterType<Menu>().As<IMenu>();

            return containerBuilder.Build();
        }

    }
}
