using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Services.Guests.GuestServices.Delete;

public class DeleteGuest : IDelete
{
    private readonly IHardDeleteItem _hardDeleteItem;

    public DeleteGuest(IHardDeleteItem hardDeleteItem)
    {
        _hardDeleteItem = hardDeleteItem;
    }
    public void Delete()
    {
        _hardDeleteItem.HardDelete("guest");

    }
}
