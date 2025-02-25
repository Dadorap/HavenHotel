﻿using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Utilities.RoomsMenus;

public class DeleteRoomMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDelete _delete;
    private readonly ISoftDelete _softDelete;
    private readonly IUnDelete _unDelete;

    public DeleteRoomMenu
    (
        ISharedMenu menu,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        [KeyFilter("DeleteRoom")] IDelete delete,
        [KeyFilter("SoftDeleteRoom")] ISoftDelete softDelete,
        [KeyFilter("UnDeleteRoom")] IUnDelete unDelete
    )
    {
        _menu = menu;
        _mainMenu = mainMenu;
        _delete = delete;
        _softDelete = softDelete;
        _unDelete = unDelete;
    }

    public void DisplayMenu()
    {
        var deletedRoomMenu = new List<string>
        {
            "Soft Delete Room",
            "Delete Room",
            "Undelete Room",
            "Return to Main Menu"
        };

        _menu.DisplayMenu(
            "Deleted Rooms Menu",
            deletedRoomMenu,
            _softDelete.SoftDelete,
            _delete.Delete,
            _unDelete.UndoDete,
            _mainMenu.Value.DisplayMenu
        );
    }

}
