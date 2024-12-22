using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;

namespace HavenHotel.Utilities;

public class NavigationHelper : INavigationHelper
{
    private readonly IMenu _mainMenu;

    public NavigationHelper([KeyFilter("MainMenu")] IMenu mainMenu)
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
