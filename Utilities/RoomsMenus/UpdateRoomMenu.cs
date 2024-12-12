using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.RoomsInterfaces;

namespace HavenHotel.Utilities.RoomsMenus;

public class UpdateRoomMenu : IMenu
{
    private readonly ISharedMenu _menu;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IUpdateRoom _extraBeds;
    private readonly IUpdateRoom _roomPrice;
    private readonly IUpdateRoom _roomNumber;
    private readonly IUpdateRoom _roomSize;
    private readonly IUpdateRoom _roomType;
    private readonly IUpdateRoom _roomTotalGuests;

    public UpdateRoomMenu
        (
        ISharedMenu menu,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        [KeyFilter("ExtraBedUpdate")] IUpdateRoom extraBeds,
        [KeyFilter("PriceUpdate")] IUpdateRoom roomPrice,
        [KeyFilter("RoomNumberUpdate")] IUpdateRoom roomNumber,
        [KeyFilter("SizeUpdate")] IUpdateRoom roomSize,
        [KeyFilter("RoomTypeUpdate")] IUpdateRoom roomType,
        [KeyFilter("TotalGuestsUpdate")] IUpdateRoom roomTotalGuests
        )
    {
        _menu = menu;
        _mainMenu = mainMenu;
        _extraBeds = extraBeds;
        _roomPrice = roomPrice;
        _roomNumber = roomNumber;
        _roomSize = roomSize;
        _roomType = roomType;
        _roomTotalGuests = roomTotalGuests;
    }

    public void DisplayMenu()
    {
        var updateMenu = new List<string>
        {
            "Update Price",
            "Update Room Size",
            "Update Room Type",
            "Update Extra Beds",
            "Update Room Number",
            "Update Total Guests",
            "Return to Main Menu"
        };


        _menu.DisplayMenu
            (
            "Update Room Details",
            updateMenu,
            _roomPrice.UpdateRoom,
            _roomSize.UpdateRoom,
            _roomType.UpdateRoom,
            _extraBeds.UpdateRoom,
            _roomNumber.UpdateRoom,
            _roomTotalGuests.UpdateRoom,
            _mainMenu.Value.DisplayMenu
            );
    }
}
