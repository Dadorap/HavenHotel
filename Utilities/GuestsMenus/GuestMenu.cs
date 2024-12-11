using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Utilities.GuestsMenus
{
    public class GuestMenu : IMenu
    {
        private readonly Lazy<IMenu> _mainMenu;
        private readonly ISharedMenu _menu;
        private readonly ICreate _create;
        private readonly Lazy<IMenu> _display;
        private readonly Lazy<IMenu> _update;
        private readonly Lazy<IMenu> _delete;

        public GuestMenu
            (
           ISharedMenu menu,
           [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
           [KeyFilter("CreateGuest")] ICreate create,
           [KeyFilter("DisplayGuestMenu")] Lazy<IMenu> display,
           [KeyFilter("UpdateGuestMenu")] Lazy<IMenu> update,
           [KeyFilter("DeletedGuestMenu")] Lazy<IMenu> delete
            )
        {
            _mainMenu = mainMenu;
            _menu = menu;
            _create = create;
            _display = display;
            _update = update;
            _delete = delete;
        }
        public void DisplayMenu()
        {
            var guestMenuList = new List<string>
            {
                "New Guest",
                "View Guest",
                "Edit Guest",
                "Delete Guest",
                "Main Menu"
            };

            _menu.DisplayMenu(
                "guest menu",
                guestMenuList,
                _create.Create,
                _display.Value.DisplayMenu,
                _update.Value.DisplayMenu,
                _delete.Value.DisplayMenu,
                _mainMenu.Value.DisplayMenu
            );

        }
    }
}

