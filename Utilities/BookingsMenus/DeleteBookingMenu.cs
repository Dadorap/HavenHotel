﻿using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Utilities.BookingsMenus;

public class DeleteBookingMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDelete _delete;
    private readonly ISoftDelete _softDelete;
    private readonly IUnDelete _unDelete;

    public DeleteBookingMenu
    (
        ISharedMenu menu,
       [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
       [KeyFilter("DeleteBooking")] IDelete delete,
       [KeyFilter("SoftDeleteBooking")] ISoftDelete softDelete,
       [KeyFilter("UnDeleteBooking")] IUnDelete unDelete

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
        var deletedBookingMenu = new List<string>
        {
            "Soft Delete Booking",
            "Delete Booking",
            "Undelete Booking",
            "Return to Main Menu"
        };
        string header = "Deleted Bookings Menu";

        _menu.DisplayMenu(header, deletedBookingMenu, _softDelete.SoftDelete, _delete.Delete, _unDelete.UndoDete, _mainMenu.Value.DisplayMenu);


    }
}
