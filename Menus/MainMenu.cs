using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;


namespace HavenHotel.Menus;

public class MainMenu : IMenu
{
    private readonly ISharedMenu _mainMenu;
    private readonly IExit _exit;
    private readonly IMenu _guestMenu;
    private readonly IMenu _roomMenu;
    private readonly IMenu _bookingMenu;

    public MainMenu(ISharedMenu mainMenu, 
        IExit exit, 
        [KeyFilter("GuestMenu")] IMenu guestMenu,
        [KeyFilter("RoomMenu")] IMenu roomMenu,
        [KeyFilter("BookingMenu")] IMenu bookingMenu)
    {
        _mainMenu = mainMenu;
        _exit = exit;
        _guestMenu = guestMenu;
        _roomMenu = roomMenu;
        _bookingMenu = bookingMenu;
    }

    public void DisplayMenu()
    {
        int currentSelect = 0;
        List<string> menu = new()
        {
            "Rooms",
            "Guests",
            "Bookings",
            "Exit"
        };

        _mainMenu.DisplayMenu("Main Menu",menu, _roomMenu.DisplayMenu, _guestMenu.DisplayMenu, _bookingMenu.DisplayMenu, _exit.ExitConsole);


    }

}
