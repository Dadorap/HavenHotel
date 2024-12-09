using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Services.Guests.GuestServices.Delete;

public class SoftDeleteGuest : ISoftDelete
{
    private readonly ISoftDeleteItem _item;
    public SoftDeleteGuest(ISoftDeleteItem softDeleteItem)
    {
        _item = softDeleteItem;
    }
    public void SoftDelete()
    {
        _item.SoftDelete("guest");
    }
}
