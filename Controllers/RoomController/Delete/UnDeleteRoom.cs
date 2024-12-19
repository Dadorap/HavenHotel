using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Controllers.RoomController.Delete;

public class UnDeleteRoom : IUnDelete
{
    private readonly IUnDeleteItem _unDeleteItem;
    public UnDeleteRoom(IUnDeleteItem unDeleteItem)
    {
        _unDeleteItem = unDeleteItem;
    }
    public void UndoDete()
    {
        _unDeleteItem.UnDelete("ROOM");
    }
}
