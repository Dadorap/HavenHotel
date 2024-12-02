using HavenHotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Common
{
    public class NavigationHelper : INavigationHelper
    {
        private readonly IMainMenu _mainMenu;

        public NavigationHelper(IMainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }

        public void ReturnToMenu(string input)
        {
            if (input.ToLower() == "cancel")
            {
                _mainMenu.DisplayMenu();
                return;
            }
        }
    }

}
