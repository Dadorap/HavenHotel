using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Services.Guests
{
    public class UpdateConfirmation : IUpdateConfirmation
    {
        private readonly Lazy<IMenu> _mainMenu;

        public UpdateConfirmation
            (
            [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu
            )
        {
            _mainMenu = mainMenu;
        }

        public void Confirmation(string input)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ResetColor();
            Console.Write("Press any key to return to menu...");
            Console.ReadKey();
            _mainMenu.Value.DisplayMenu();
        }
    }
}
