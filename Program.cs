using Autofac;
using HavenHotel.Configuration;
using HavenHotel.Interfaces;

namespace HavenHotel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyContainer.Configure();

            var menu = container.Resolve<IMainMenu>();

            menu.DisplayMenu();
        }
    }
}
