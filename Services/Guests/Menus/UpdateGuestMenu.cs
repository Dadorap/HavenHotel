using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Services.Guests.Menus;

public class UpdateGuestMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly INameUpdate _nameUpdate;
    private readonly IEmailUpdate _emailUpdate;
    private readonly IPhoneNumberUpdate _phoneNumberUpdate;

    public UpdateGuestMenu
        (
        ISharedMenu menu,
       [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        INameUpdate nameUpdate,
        IEmailUpdate emailUpdate, 
        IPhoneNumberUpdate phoneNumberUpdate)
    {
        _menu = menu;
        _mainMenu = mainMenu;
        _nameUpdate = nameUpdate;
        _emailUpdate = emailUpdate;
        _phoneNumberUpdate = phoneNumberUpdate;
    }

    public void DisplayMenu()
    {
        var updateMenu = new List<string>
        {
            "Update Guest's Name",
            "Update Guest's Email",
            "Update Guest's Phone Number",
            "Return to Main Menu"
        };

        _menu.DisplayMenu
            (
            "Update Guest Details", 
            updateMenu, 
            _nameUpdate.NameUpdater,
            _emailUpdate.EmailUpdater, 
            _phoneNumberUpdate.PhoneNumberUpdater,
            _mainMenu.Value.DisplayMenu
            );

    }
}
