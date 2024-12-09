using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Services.Guests.Menus;

public class DeletedGuestMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDelete _delete;
    private readonly ISoftDelete _softDelete;
    private readonly IUnDelete _unDelete;

    public DeletedGuestMenu
    (
        ISharedMenu menu,
       [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
       [KeyFilter("DeleteGuest")] IDelete delete,
       [KeyFilter("SoftDeleteGuest")] ISoftDelete softDelete,
       [KeyFilter("UnDeleteGuest")] IUnDelete unDelete

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
        var deletedGuestMenu = new List<string>
        {
            "Soft Delete Guest",
            "Delete Guest",
            "Undelete Guest",
            "Return to Main Menu"
        };

        _menu.DisplayMenu(
            "Deleted Guests Menu",
            deletedGuestMenu,
            _softDelete.SoftDelete,
            _delete.Delete,
            _unDelete.UndoDete,
            _mainMenu.Value.DisplayMenu
        );
    }
}
