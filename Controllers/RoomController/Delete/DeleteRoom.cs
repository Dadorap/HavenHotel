using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Controllers.RoomController.Delete;

public class DeleteRoom : IDelete
{
    private readonly IHardDeleteItem _hardDeleteItem;

    public DeleteRoom(IHardDeleteItem hardDeleteItem)
    {
        _hardDeleteItem = hardDeleteItem;
    }
    public void Delete()
    {
        _hardDeleteItem.HardDelete("room");

    }
}
