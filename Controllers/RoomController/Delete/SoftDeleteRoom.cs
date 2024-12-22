using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Controllers.RoomController.Delete;

public class SoftDeleteRoom : ISoftDelete
{
    private readonly ISoftDeleteItem _item;
    public SoftDeleteRoom(ISoftDeleteItem softDeleteItem)
    {
        _item = softDeleteItem;
    }
    public void SoftDelete()
    {
        _item.SoftDelete("room");
    }
}
